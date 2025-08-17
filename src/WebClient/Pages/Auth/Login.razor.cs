using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using WebClient.DTOs;

namespace WebClient.Pages.Auth;

public partial class Login(HttpClient http, IJSRuntime js,NavigationManager nav)
{
    private readonly DTOs.User.Auth.Login _model = new();
    private string _errorMessage=string.Empty;
    private string _successMessage=string.Empty;
    private bool _isLoading;
    [Inject] private IJSRuntime Js { get; set; } = js;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _successMessage = await js.InvokeAsync<string>(identifier: "localStorage.getItem", "RegisterSuccess");
        if (!string.IsNullOrEmpty(_successMessage))
        {
            await js.InvokeVoidAsync(identifier: "localStorage.removeItem", "RegisterSuccess");
        }
    }

    private async Task LoginUser()
    {

        _successMessage = string.Empty;

        _isLoading = true;
        _errorMessage = string.Empty;

        try
        {
            var response = await http.PostAsJsonAsync(requestUri: "Account/Login", _model);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse<string>>();

            if (!result.IsSuccess)
            {
                _errorMessage = result.Message?? "Login operation failed";
            }
            else
            {

                var token = result?.Data;
                await js.InvokeVoidAsync(identifier: "localStorage.setItem", "authToken", token);
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: "Bearer", token);

                await js.InvokeVoidAsync(identifier: "localStorage.setItem", "loginSuccess", $"{result.Message}");
                nav.NavigateTo(uri: "/User/Profile");
            }
        }
        finally
        {
            _isLoading = false;
        }
    }
}