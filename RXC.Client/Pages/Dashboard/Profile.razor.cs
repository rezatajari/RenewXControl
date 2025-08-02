using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using RXC.Client.DTOs;

namespace RXC.Client.Pages.Dashboard
{
    public partial class Profile
    {
        private DTOs.User.Profile? profile;
        private string? successMessage;
        private string? errorMessage;

        protected override async Task OnInitializedAsync()
        {
            successMessage = await JS.InvokeAsync<string>("localStorage.getItem", "loginSuccess");
            if (!string.IsNullOrEmpty(successMessage))
            {
                await JS.InvokeVoidAsync("localStorage.removeItem", "loginSuccess");
                StateHasChanged();
            }

            var token = await JS.InvokeAsync<string>("localStorage.getItem", "authToken");

            if (string.IsNullOrEmpty(token))
            {
                Nav.NavigateTo("/login");
                return;
            }

            try
            {
                Http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var response = await Http.GetFromJsonAsync<GeneralResponse<DTOs.User.Profile>>("api/Dashboard/profile");

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

        private void NavigateToEditProfile()
        {
            Nav.NavigateTo("/dashboard/profile/edit");
        }

        private void NavigateToChangePassword()
        {
            Nav.NavigateTo("/dashboard/profile/change-password");
        }
    }
}
