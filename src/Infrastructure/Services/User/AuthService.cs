using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common;
using Application.DTOs.User.Auth;
using Application.Interfaces.User;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.User;

public class AuthService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    RoleManager<IdentityRole<Guid>> roleManager,
    IOptions<JwtSettings> jwtSettings)
    : IAuthService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public async Task<GeneralResponse<string>> RegisterAsync(Register register)
    {
        var userExist = await userManager.FindByEmailAsync(register.Email);
        if (userExist != null)
        {
            return GeneralResponse<string>.Failure(
                message: "Registration failed",
                errors:
                [
                    new ErrorResponse
                    {
                        Name = "Email",
                        Message= "Email is already registered"
                    }
                ]);
        }

        var appUser = new ApplicationUser
        {
            UserName = register.UserName,
            Email = register.Email,
            ProfileImage = null,
            CreateTime = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(appUser, register.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => new ErrorResponse
            {
                Name = "Create user",
                Message = e.Description
            }).ToList();

            return GeneralResponse<string>.Failure(message:"Creation user is failed",errors:errors);
        }

        if (!await roleManager.RoleExistsAsync("User"))
        {
            var role = new IdentityRole<Guid>
            {
                Id = Guid.NewGuid(),
                Name = "User",
                NormalizedName = "USER"
            };
            await roleManager.CreateAsync(role);
        }

        await userManager.AddToRoleAsync(appUser, role: "User");

        return GeneralResponse<string>.Success(data:null, message:"User registered successfully");
    }

    public async Task<GeneralResponse<string>> LoginAsync(Login login)
    {
        var user = await userManager.FindByEmailAsync(login.Email);
        if (user == null)
        {
            return GeneralResponse<string>.Failure(
                message: "Login operation is failed",
                errors:
                [
                    new()
                    {
                        Name = "User invalid",
                        Message = $"User by this email: {login.Email} is not exist"
                    }
                ]
            );
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, login.Password, false);
        if (!result.Succeeded)
            return GeneralResponse<string>.Failure(
                message:"Invalid credentials", 
                errors: [
                    new ErrorResponse
                    {
                        Name = "Password",
                        Message = "Your is locked or not allowed"
                    }]);

        var roles = await userManager.GetRolesAsync(user);

        var authUser=new AuthUser
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };


        var token = GenerateToken(authUser, roles);

        return GeneralResponse<string>.Success(data:token, message:"Login successful");
    }

    public async Task<GeneralResponse<bool>> LogoutAsync()
    {
       await signInManager.SignOutAsync();

       return GeneralResponse<bool>.Success(data: true);
    }

    public string GenerateToken(AuthUser user, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(key,algorithm: SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenLifetimeMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<GeneralResponse<bool>> ChangePasswordAsync(ChangePassword changePassword, Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        var result = await userManager.ChangePasswordAsync(
            user,
            changePassword.CurrentPassword,
            changePassword.NewPassword);

        if (result.Succeeded)
            return GeneralResponse<bool>.Success(
                data: true,
                message: "Your update password successful"
            );


        var errors=result.Errors
            .Select(error=>new ErrorResponse(
                name:error.Code.ToString(),
                message:error.Description))
            .ToList();

        return GeneralResponse<bool>.Failure(
            message: "Password update failed.",
            errors: errors);
    }
}