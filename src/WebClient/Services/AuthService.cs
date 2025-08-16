using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WebClient.DTOs;
using WebClient.DTOs.User.Auth;
using WebClient.DTOs.User.Profile;

namespace WebClient.Services;

public class AuthService(HttpClient http, NavigationManager nav, IJSRuntime js)
{
    public async Task<GeneralResponse<string>> LoginAsync(Login model)
    {
        var result = await http.PostAsJsonAsync(requestUri:"api/auth/login", model);
        var response = await result.Content.ReadFromJsonAsync<GeneralResponse<string>>();

        if (!result.IsSuccessStatusCode)
            return response;

        var token = response.Data;
        await js.InvokeVoidAsync(identifier:"localStorage.setItem", "authToken", token);
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme:"Bearer", token);
        nav.NavigateTo(uri:"/dashboard/profile");
        return response;
    }

    public async Task<GeneralResponse<string>> RegisterAsync(Register model)
    {
        var result= await http.PostAsJsonAsync(requestUri:"Account/Register", model);
        return await  result.Content.ReadFromJsonAsync<GeneralResponse<string>>();
    }

    public async Task<GeneralResponse<bool>> LogoutAsync()
    {
        var result=  await http.PostAsync("api/auth/logout", null);
        var resultContent = await result.Content.ReadFromJsonAsync<GeneralResponse<bool>>();

        if (!resultContent.IsSuccess)
            return new GeneralResponse<bool>()
            {
                IsSuccess = false, 
                Message = "Log out is not work"
            };

        await js.InvokeVoidAsync("localStorage.removeItem", "authToken");

        http.DefaultRequestHeaders.Authorization = null;

        return new GeneralResponse<bool>
        {
            IsSuccess = true,
            Data = true,
            Message = "Logged out successfully"
        };
    }

    public async Task<GeneralResponse<bool>> ChangePasswordAsync(ChangePassword changePassword)
    {

        var result = await http.PutAsJsonAsync(
            requestUri: "api/auth/change-password",
            value: changePassword);

        var resultContent = await result.Content.ReadFromJsonAsync<GeneralResponse<bool>>();
        if (!resultContent.IsSuccess)
            return new GeneralResponse<bool>()
            {
                IsSuccess = false,
                Message = resultContent.Message,
                Errors = resultContent.Errors
            };

        return new GeneralResponse<bool>()
        {
            Data = resultContent.Data,
            IsSuccess = true,
            Message = resultContent.Message
        };
    }

    public async Task<GeneralResponse<bool>> EditProfileAsync(EditProfile editProfile)
    {
        try
        {
            var response = await http.PutAsJsonAsync(
                "api/dashboard/profile/edit",
                editProfile);

            if (!response.IsSuccessStatusCode)
            {
                return new GeneralResponse<bool>
                {
                    IsSuccess = false,
                    Message = $"Server error: {response.StatusCode}"
                };
            }

            var resultContent = await response.Content.ReadFromJsonAsync<GeneralResponse<bool>>();
            return resultContent ?? new GeneralResponse<bool>
            {
                IsSuccess = false,
                Message = "Invalid server response"
            };
        }
        catch (Exception ex)
        {
            return new GeneralResponse<bool>
            {
                IsSuccess = false,
                Message = $"Network error: {ex.Message}"
            };
        }
    }
}