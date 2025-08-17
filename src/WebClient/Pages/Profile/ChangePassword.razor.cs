using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WebClient.DTOs;

namespace WebClient.Pages.Profile;

public partial class ChangePassword(HttpClient http, IJSRuntime js, NavigationManager nav)
{
    private readonly DTOs.User.Auth.ChangePassword _model = new();
    private bool _isLoading = false;
    private string? _errorMessage = string.Empty;

    private async Task ChangePass()
    {
        _isLoading = true;
        _errorMessage = null;

        try
        {
            var response = await http.PutAsJsonAsync(
                requestUri: "Users/User/Change-Password",
                value: _model);

            var result= await response.Content.ReadFromJsonAsync<GeneralResponse<bool>>();

            if (!result.IsSuccess)
            {
                _errorMessage = result.Message;
            }
            else
            {
                await js.InvokeVoidAsync(identifier: "localStorage.setItem", "passwordChangeSuccess", $"{result.Message}");
                nav.NavigateTo(uri: "/User/Profile");
            }
        }
        catch (Exception ex)
        {
            _errorMessage = "An error occurred while changing password";
        }
        finally
        {
            _isLoading = false;
        }

    }
}