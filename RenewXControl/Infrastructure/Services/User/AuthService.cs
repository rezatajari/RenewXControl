using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RenewXControl.Api.Utility;
using RenewXControl.Application.DTOs.User.Auth;
using RenewXControl.Application.User.Interfaces;

namespace RenewXControl.Infrastructure.Services.User;

public class AuthService : IAuthService
{
    private readonly UserManager<Domain.User.User> _userManager;
    private readonly SignInManager<Domain.User.User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        UserManager<Domain.User.User> userManager,
        SignInManager<Domain.User.User> signInManager, 
        RoleManager<IdentityRole> roleManager,
        IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<GeneralResponse<string>> RegisterAsync(Register register)
    {
        var userExist = await _userManager.FindByEmailAsync(register.Email);
        if (userExist != null)
        {
            return GeneralResponse<string>.Failure(
                message: "Registration failed",
                errors:
                [
                    new()
                    {
                        Name = "Email",
                        Message= "Email is already registered"
                    }
                ]);
        }

        var user = Domain.User.User.Create(register.UserName, register.Email);
        var result = await _userManager.CreateAsync(user, register.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => new ErrorResponse
            {
                Name = "Create user",
                Message = e.Description
            }).ToList();

            return GeneralResponse<string>.Failure(message:"Creation user is failed",errors:errors);
        }

        if (!await _roleManager.RoleExistsAsync("User"))
            await _roleManager.CreateAsync(new IdentityRole("User"));

        await _userManager.AddToRoleAsync(user,role: "User");

        return GeneralResponse<string>.Success(data:null, message:"User registered successfully");
    }

    public async Task<GeneralResponse<string>> LoginAsync(Login login)
    {
        var user = await _userManager.FindByEmailAsync(login.Email);
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

        var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
        if (!result.Succeeded)
            return GeneralResponse<string>.Failure(
                message:"Invalid credentials", 
                errors: [
                    new ErrorResponse
                    {
                        Name = "Password",
                        Message = "Your is locked or not allowed"
                    }]);

        var roles = await _userManager.GetRolesAsync(user);
        var token = GenerateToken(user, roles);

        return GeneralResponse<string>.Success(data:token, message:"Login successful");
    }

    public async Task<GeneralResponse<bool>> LogoutAsync()
    {
       await _signInManager.SignOutAsync();

       return GeneralResponse<bool>.Success(data: true);
    }

    public string GenerateToken(Domain.User.User user, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
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
            claims = claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenLifetimeMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}