using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WebClient.DTOs;

namespace WebClient.Pages.Asset;

public partial class AddTurbine(HttpClient http,IJSRuntime js,NavigationManager nav)
{
    private DTOs.AddAsset.AddTurbine turbineModel = new();
    private bool isLoading = false;
    private bool showSuccess = false;
    private string? errorMessage;
    private string turbineModelName = string.Empty;
    private DateTime? installationDate = DateTime.Today;
    private string notes = string.Empty;
    private int redirectProgress = 0;

    private async Task HandleSubmit()
    {
        isLoading = true;
        errorMessage = null;

        try
        {
            var token = await js.InvokeAsync<string>("localStorage.getItem", "authToken");
            if (string.IsNullOrEmpty(token))
            {
                nav.NavigateTo("/login");
                return;
            }

            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await http.PostAsJsonAsync("api/asset/add/turbine", turbineModel);

            if (response.IsSuccessStatusCode)
            {
                showSuccess = true;
                await Task.Delay(2000);
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<GeneralResponse<string>>();
                errorMessage = errorResponse?.Message ?? "Failed to add wind turbine";
            }
        }
        catch (Exception ex)
        {
            errorMessage = $"An error occurred: {ex.Message}";
            Console.Error.WriteLine($"Add Turbine Error: {ex}");
        }
        finally
        {
            isLoading = false;
        }
    }

    private void Cancel()
    {
        nav.NavigateTo("/dashboard/profile");
    }
}