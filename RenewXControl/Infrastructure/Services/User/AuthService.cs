using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RenewXControl.Api.Utility;
using RenewXControl.Application.DTOs.User.Auth;
using RenewXControl.Application.User;

namespace RenewXControl.Infrastructure.Services.User;

public class AuthService:IAuthService
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthService(UserManager<Domain.User> userManager,SignInManager<Domain.User> signInManager,RoleManager<IdentityRole> roleManager)
    {
        _userManager= userManager;
        _signInManager= signInManager;
        _roleManager= roleManager;
    }

    public async Task<GeneralResponse<string>> RegisterAsync(Register register)
    {
        if (register.Password != register.ConfirmPassword)
            return GeneralResponse<string>.Failure("Password do not match");

        var user = Domain.User.Create(register.UserName, register.Email);
        var result=await _userManager.CreateAsync(user,register.Password);

        if (!result.Succeeded)
            return GeneralResponse<string>.Failure(result.Errors.First().Description);

        if (!await _roleManager.RoleExistsAsync("User"))
            await _roleManager.CreateAsync(new IdentityRole("User"));

        await _userManager.AddToRoleAsync(user, "User");

        return GeneralResponse<string>.Success(null,"User registered successfully");
    }

    public async Task<GeneralResponse<string>> LoginAsync(Login login)
    {
        var user = await _userManager.FindByEmailAsync(login.Email);
        if (user == null) 
            return GeneralResponse<string>.Failure("Invalid credentials");

        var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
        if (!result.Succeeded)
            return GeneralResponse<string>.Failure("Invalid credentials");

        var roles = await _userManager.GetRolesAsync(user);
        var token = GenerateToken(user, roles);

        return GeneralResponse<string>.Success(token, "Login successful");
    }

    public string GenerateToken(Domain.User user, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role,role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("5aS*Qm#_^P+zm\\\"c<+g2wk>iOfEm38{BHmG!QG)"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "RxcService",
            audience: "Users",
            claims = claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}