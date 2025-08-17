using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WebClient.DTOs;

namespace WebClient.Pages.Asset;

public partial class AddBattery(HttpClient http, IJSRuntime js, NavigationManager nav)
{
    private readonly DTOs.AddAsset.AddBattery _batteryModel = new();
    private bool _isLoading;
    private bool _showSuccess;
    private string? _errorMessage;

    private async Task HandleSubmit()
    {
        _isLoading = true;
        _errorMessage = null;

        try
        {
            var token = await js.InvokeAsync<string>("localStorage.getItem", "authToken");
            if (string.IsNullOrEmpty(token))
            {
                nav.NavigateTo("/login");
                return;
            }

            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await http.PostAsJsonAsync(requestUri:"Assets/battery", _batteryModel);

            if (response.IsSuccessStatusCode)
            {
                _showSuccess = true;
                await Task.Delay(2000); 
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<GeneralResponse<string>>();
                _errorMessage = errorResponse?.Message ?? "Failed to add battery";
            }
        }
        catch (Exception ex)
        {
            _errorMessage = $"An error occurred: {ex.Message}";
        }
        finally
        {
            _isLoading = false;
        }
    }

    private void Cancel()
    {
        nav.NavigateTo("/User/Profile");
    }
}