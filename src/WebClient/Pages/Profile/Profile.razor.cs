using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using WebClient.DTOs;

namespace WebClient.Pages.Profile;

public partial class Profile
{
    private DTOs.User.Profile.Profile? profile;
    private string? successMessage;
    private string? errorMessage;

    protected override async Task OnInitializedAsync()
    {
        var token = await JS.InvokeAsync<string>("localStorage.getItem", "authToken");
        if (string.IsNullOrEmpty(token))
        {
            Nav.NavigateTo("/login");
            return;
        }

        var passwordChangeMsg = await JS.InvokeAsync<string>("localStorage.getItem", "passwordChangeSuccess");
        var loginSuccessMsg = await JS.InvokeAsync<string>("localStorage.getItem", "loginSuccess");
        var editProfileMsg = await JS.InvokeAsync<string>("localStorage.getItem", "EditProfileSuccess");

        successMessage = passwordChangeMsg ?? loginSuccessMsg ?? editProfileMsg;

        if (!string.IsNullOrEmpty(passwordChangeMsg))
            await JS.InvokeVoidAsync("localStorage.removeItem", "passwordChangeSuccess");

        if (!string.IsNullOrEmpty(loginSuccessMsg))
            await JS.InvokeVoidAsync("localStorage.removeItem", "loginSuccess");

        if (!string.IsNullOrEmpty(editProfileMsg))
            await JS.InvokeVoidAsync("localStorage.removeItem", "EditProfileSuccess");

        if (!string.IsNullOrEmpty(successMessage))
        {
            StateHasChanged();

            await Task.Delay(5000);
            successMessage = null;
            StateHasChanged();
        }

        try
        {
            Http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await Http.GetFromJsonAsync<GeneralResponse<DTOs.User.Profile.Profile>>("api/Dashboard/profile");

            if (response == null || !response.IsSuccess)
            {
                errorMessage = response?.Message ?? "Failed to load profile";
            }
            else
            {
                profile = response.Data;
                if (!string.IsNullOrEmpty(response.Message))
                {
                    successMessage = response.Message;
                }
            }
        }
        catch (Exception ex)
        {
            errorMessage = "An error occurred while loading profile";
            Console.WriteLine("API Error: " + ex.Message);
        }
    }
    private string GetProfileImageUrl(string imagePath)
    {
        if (string.IsNullOrEmpty(imagePath))
            return string.Empty;

        // If it's already a full URL (like from a CDN), return as-is
        if (imagePath.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            return imagePath;

        // Handle both wwwroot/profile-images and custom profile-images cases
        if (imagePath.StartsWith("profile-images/"))
        {
            return $"{Nav.BaseUri}{imagePath}";
        }

        // Default case - assume it's in wwwroot
        return $"{Nav.BaseUri}{imagePath.TrimStart('/')}";
    }
    private void NavigateToEditProfile()
    {
        Nav.NavigateTo("/dashboard/profile/edit");
    }

    private void NavigateToChangePassword()
    {
        Nav.NavigateTo("/dashboard/profile/change-password");
    }
}