using Microsoft.AspNetCore.Identity;
using RenewXControl.Api.Utility;
using RenewXControl.Application.DTOs.User.Auth;
using RenewXControl.Application.User;
using RenewXControl.Domain.Users;

namespace RenewXControl.Infrastructure.Services.User;

public class AuthService:IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthService(UserManager<User> userManager,SignInManager<User> signInManager,RoleManager<IdentityRole> roleManager)
    {
        _userManager= userManager;
        _signInManager= signInManager;
        _roleManager= roleManager;
    }

    public async Task<GeneralResponse<string>> RegisterAsync(Register register)
    {
        if (register.Password != register.ConfirmPassword)
            return GeneralResponse<string>.Failure("Password do not match");

        var user = User.Create(register.UserName, register.Email);
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

        return GeneralResponse<string>.Success(null, "Login successful");
    }
}