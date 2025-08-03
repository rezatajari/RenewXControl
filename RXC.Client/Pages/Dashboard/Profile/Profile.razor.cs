using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using RXC.Client.DTOs;

namespace RXC.Client.Pages.Dashboard.Profile
{
    public partial class Profile
    {
        private DTOs.User.Profile.Profile? profile;
        private string? successMessage;
        private string? errorMessage;

        protected override async Task OnInitializedAsync()
        {
            var token = await JS.InvokeAsync<string>("localStorage.getItem", "authToken");
            if (string.IsNullOrEmpty(token))
            {
                Nav.NavigateTo("/login");
                return;
            }

            var passwordChangeMsg = await JS.InvokeAsync<string>("localStorage.getItem", "passwordChangeSuccess");
            var loginSuccessMsg = await JS.InvokeAsync<string>("localStorage.getItem", "loginSuccess");
            var editProfileMsg = await JS.InvokeAsync<string>("localStorage.getItem", "EditProfileSuccess");
            

            if (!string.IsNullOrEmpty(passwordChangeMsg))
            {
                successMessage = passwordChangeMsg;
                await JS.InvokeVoidAsync("localStorage.removeItem", "passwordChangeSuccess");
                StateHasChanged();
            }
            else if (!string.IsNullOrEmpty(loginSuccessMsg))
            {
                successMessage = loginSuccessMsg;
                await JS.InvokeVoidAsync("localStorage.removeItem", "loginSuccess");
            } else if (!string.IsNullOrEmpty(editProfileMsg))
            {
                successMessage=editProfileMsg;
                await JS.InvokeVoidAsync("LocalStorage.removeItem", "EditProfileSuccess");
            }

            if (!string.IsNullOrEmpty(successMessage))
            {
                StateHasChanged();

                await Task.Delay(5000);
                successMessage = null;
                StateHasChanged();
            }

            try
            {
                Http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

                var response = await Http.GetFromJsonAsync<GeneralResponse<DTOs.User.Profile.Profile>>("api/Dashboard/profile");

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
