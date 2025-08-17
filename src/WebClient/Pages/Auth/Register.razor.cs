using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WebClient.DTOs;

namespace WebClient.Pages.Auth;

public partial class Register(HttpClient http, IJSRuntime js, NavigationManager nav)
{
    private readonly DTOs.User.Auth.Register _model=new();
    private string _errorMessage = string.Empty;
    private bool _isLoading;
    [Inject] private IJSRuntime Js { get; set; } = js;
    [Inject] private NavigationManager Nav { get; set; } = default!;
    private async Task RegisterUser()
    {
        _isLoading = true;
        _errorMessage = string.Empty;

        try
        {
            var response = await http.PostAsJsonAsync(requestUri: "Account/Register", _model);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse<string>>();

            if (!result.IsSuccess)
            {
                _errorMessage = result.Message ?? "Registration failed";
            }
            else
            {
                await js.InvokeVoidAsync("localStorage.setItem", "RegisterSuccess", result.Message ?? "Registration successful");
                nav.NavigateTo("/login");
            }
        }
        catch (Exception ex)
        {
            _errorMessage = "An error occurred during registration";
        }
        finally
        {
            _isLoading = false;
        }
    }
}