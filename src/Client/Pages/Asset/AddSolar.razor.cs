using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using RXC.Client.DTOs;

namespace RXC.Client.Pages.Asset
{
    public partial class AddSolar
    {
        private DTOs.AddAsset.AddSolar solarModel = new();
        private bool isLoading = false;
        private bool showSuccess = false;
        private string? errorMessage;
        private string panelType = string.Empty;
        private DateTime? installationDate = DateTime.Today;

        private async Task HandleSubmit()
        {
            isLoading = true;
            errorMessage = null;

            try
            {
                var token = await JS.InvokeAsync<string>("localStorage.getItem", "authToken");
                if (string.IsNullOrEmpty(token))
                {
                    Nav.NavigateTo("/login");
                    return;
                }

                Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await Http.PostAsJsonAsync("api/asset/add/solar", solarModel);

                if (response.IsSuccessStatusCode)
                {
                    showSuccess = true;
                    await Task.Delay(2000); // Show success message for 2 seconds
                    Nav.NavigateTo("/monitoring");
                }
                else
                {
                    var errorResponse = await response.Content.ReadFromJsonAsync<GeneralResponse<string>>();
                    errorMessage = errorResponse?.Message ?? "Failed to add solar panel";
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"An error occurred: {ex.Message}";
                Console.Error.WriteLine($"Add Solar Error: {ex}");
            }
            finally
            {
                isLoading = false;
            }
        }

        private void Cancel()
        {
            Nav.NavigateTo("/monitoring");
        }
    }
}