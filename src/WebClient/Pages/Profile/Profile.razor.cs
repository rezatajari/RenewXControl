using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WebClient.DTOs;

namespace WebClient.Pages.Profile;

public partial class Profile(HttpClient http, IJSRuntime js, NavigationManager nav)
{
    private DTOs.User.Profile.Profile? _profile;
    private string? _successMessage;
    private string? _errorMessage;

    protected override async Task OnInitializedAsync()
    {
        var token = await js.InvokeAsync<string>("localStorage.getItem", "authToken");
        if (string.IsNullOrEmpty(token))
        {
            nav.NavigateTo("/login");
            return;
        }

        var passwordChangeMsg = await js.InvokeAsync<string>("localStorage.getItem", "passwordChangeSuccess");
        var loginSuccessMsg = await js.InvokeAsync<string>("localStorage.getItem", "loginSuccess");
        var editProfileMsg = await js.InvokeAsync<string>("localStorage.getItem", "EditProfileSuccess");

        _successMessage = passwordChangeMsg ?? loginSuccessMsg ?? editProfileMsg;

        if (!string.IsNullOrEmpty(passwordChangeMsg))
            await js.InvokeVoidAsync("localStorage.removeItem", "passwordChangeSuccess");

        if (!string.IsNullOrEmpty(loginSuccessMsg))
            await js.InvokeVoidAsync("localStorage.removeItem", "loginSuccess");

        if (!string.IsNullOrEmpty(editProfileMsg))
            await js.InvokeVoidAsync("localStorage.removeItem", "EditProfileSuccess");

        if (!string.IsNullOrEmpty(_successMessage))
        {
            StateHasChanged();

            await Task.Delay(5000);
            _successMessage = null;
            StateHasChanged();
        }

        try
        {
            http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await http.GetFromJsonAsync<GeneralResponse<DTOs.User.Profile.Profile>>(requestUri:"Users/User/Profile");

            if (response is not { IsSuccess: true })
            {
                _errorMessage = response?.Message ?? "Failed to load profile";
            }
            else
            {
                _profile = response.Data;
               _successMessage = response.Message??"Load profiled successfully";
            }
        }
        catch (Exception ex)
        {
            _errorMessage = "An error occurred while loading profile";
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
            return $"{nav.BaseUri}{imagePath}";
        }

        // Default case - assume it's in wwwroot
        return $"{nav.BaseUri}{imagePath.TrimStart('/')}";
    }
    private void NavigateToEditProfile()
    {
        nav.NavigateTo("/User/Profile/Edit");
    }

    private void NavigateToChangePassword()
    {
        nav.NavigateTo("/User/Profile/Change-Password");
    }
}