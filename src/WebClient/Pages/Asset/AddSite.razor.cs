using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WebClient.DTOs;

namespace WebClient.Pages.Asset;

public partial class AddSite(HttpClient http,IJSRuntime js,NavigationManager nav)
{
    private readonly DTOs.AddAsset.AddSite _siteModel = new();
    private bool _isLoading;
    private bool _showSuccess;

    private async Task HandleSubmit()
    {
        _isLoading = true;
        try
        {
            var token = await js.InvokeAsync<string>("localStorage.getItem", "authToken");
            if (string.IsNullOrEmpty(token))
            {
                nav.NavigateTo("/login");
                return;
            }

            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await http.PostAsJsonAsync(requestUri:"Sites/Site", _siteModel);

            if (response.IsSuccessStatusCode)
            {
                _showSuccess = true;
                DashboardState.SetHasSite(true);
                await Task.Delay(2000);
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<GeneralResponse<string>>();
                await js.InvokeVoidAsync("alert", errorResponse?.Message ?? "Failed to add site");
            }
        }
        catch (Exception ex)
        {
            await js.InvokeVoidAsync("alert", $"Error: {ex.Message}");
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