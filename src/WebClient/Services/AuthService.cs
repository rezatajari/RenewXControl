using System.Net.Http.Json;
using Microsoft.JSInterop;
using WebClient.DTOs;
using WebClient.DTOs.User.Auth;
using WebClient.DTOs.User.Profile;

namespace WebClient.Services;
//TODO:Remove authservice in UI
//TODO: Refactor Profile.razor
public class AuthService(HttpClient http,IJSRuntime js)
{

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