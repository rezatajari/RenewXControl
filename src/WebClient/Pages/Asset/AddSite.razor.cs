using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using WebClient.DTOs;

namespace WebClient.Pages.Asset;

public partial class AddSite
{
    private DTOs.AddAsset.AddSite siteModel = new();
    private bool isLoading = false;
    private bool _showSuccess = false;

    private async Task HandleSubmit()
    {
        isLoading = true;
        try
        {
            var token = await JS.InvokeAsync<string>("localStorage.getItem", "authToken");
            if (string.IsNullOrEmpty(token))
            {
                Nav.NavigateTo("/login");
                return;
            }

            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await Http.PostAsJsonAsync("api/site/add", siteModel);

            if (response.IsSuccessStatusCode)
            {
                _showSuccess = true;
                DashboardState.SetHasSite(true);
                await Task.Delay(2000); // Show success message for 2 seconds
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<GeneralResponse<string>>();
                await JS.InvokeVoidAsync("alert", errorResponse?.Message ?? "Failed to add site");
            }
        }
        catch (Exception ex)
        {
            await JS.InvokeVoidAsync("alert", $"Error: {ex.Message}");
            Console.Error.WriteLine($"Add Site Error: {ex}");
        }
        finally
        {
            isLoading = false;
        }
    }

    private void Cancel()
    {
        Nav.NavigateTo("/dashboard/profile");
    }
}