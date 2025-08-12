using System.Net;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using RXC.Client.DTOs;
using RXC.Client.DTOs.User.Profile;
using RXC.Client.Services;

namespace Client.Layout
{
    public partial class DashboardLayout
    {
        private bool sidebarCollapsed = false;
        private bool showAssetsMenu = false;
        private string userName = "User";
      
        // Dynamically set sidebar classes
        private string sidebarClass => sidebarCollapsed
            ? "d-lg-none"  // Hide completely on mobile when collapsed
            : "d-flex flex-shrink-0 flex-column w-250"; // Show with fixed width

        private async Task Logout()
        {
            var result = await AuthService.LogoutAsync();
            if (!result.IsSuccess)
                await JS.InvokeVoidAsync("alert", "Logout failed: " + result.Message);

            Nav.NavigateTo("/login",forceLoad:true);
        }
        protected override async Task OnInitializedAsync()
        {
            DashboardState.OnChange += StateHasChanged;

            // Check authentication
            var token = await JS.InvokeAsync<string>("localStorage.getItem", "authToken");
            if (string.IsNullOrEmpty(token))
            {
                Nav.NavigateTo("/login");
                return;
            }

            // Load user data
            try
            {
                var response = await WebRequestMethods.Http.GetAsync("api/user/profile");
                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<Profile>();
                    userName = user?.UserName ?? "User";
                }

                // Check if user has sites
                var siteResponse = await WebRequestMethods.Http.GetAsync("api/site/HasSite");
                if (siteResponse.IsSuccessStatusCode)
                {
                    var result = await siteResponse.Content.ReadFromJsonAsync<GeneralResponse<bool>>();
                    DashboardState.SetHasSite(result?.Data ?? false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading dashboard data: {ex.Message}");
            }
        }

        public void Dispose()
        {
            DashboardState.OnChange -= StateHasChanged;
        }

        private void ToggleSidebar()
        {
            sidebarCollapsed = !sidebarCollapsed;
        }

        private void ToggleAssetsMenu()
        {
            showAssetsMenu = !showAssetsMenu;
        }
    }
}
