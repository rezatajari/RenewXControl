using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WebClient.DTOs;
using WebClient.DTOs.User.Profile;
using WebClient.Services;

namespace WebClient.Layout
{
    public partial class DashboardLayout(HttpClient http,IJSRuntime js,NavigationManager nav, DashboardState dashboardState)
    {
        private bool _sidebarCollapsed;
        private bool _showAssetsMenu;
        private string _userName = "User";


        // Dynamically set sidebar classes
        private string SidebarClass => _sidebarCollapsed
            ? "d-lg-none"  // Hide completely on mobile when collapsed
            : "d-flex flex-shrink-0 flex-column w-250"; // Show with fixed width

        private async Task Logout()
        {
            var response = await http.PostAsync("Account/Logout", null);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse<bool>>();

            if (!result.IsSuccess)
                await js.InvokeVoidAsync("alert", "Logout failed: " + result.Message);

            await js.InvokeVoidAsync("localStorage.removeItem", "authToken");
            http.DefaultRequestHeaders.Authorization = null;

            nav.NavigateTo("/login",forceLoad:true);
        }
        protected override async Task OnInitializedAsync()
        {
            dashboardState.OnChange += StateHasChanged;

            // Check authentication
            var token = await js.InvokeAsync<string>("localStorage.getItem", "authToken");
            if (string.IsNullOrEmpty(token))
            {
                nav.NavigateTo("/login");
                return;
            }

            // Load user data
            try
            {
                var response = await http.GetAsync("Users/User/Profile");
                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadFromJsonAsync<Profile>();
                    _userName = user?.UserName ?? "User";
                }

                // Check if user has sites
                var siteResponse = await http.GetAsync("Sites/User/UserId/Has-Site");
                if (siteResponse.IsSuccessStatusCode)
                {
                    var result = await siteResponse.Content.ReadFromJsonAsync<GeneralResponse<bool>>();
                    dashboardState.SetHasSite(result?.Data ?? false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading dashboard data: {ex.Message}");
            }
        }

        public void Dispose()
        {
            dashboardState.OnChange -= StateHasChanged;
        }

        private void ToggleSidebar()
        {
            _sidebarCollapsed = !_sidebarCollapsed;
        }

        private void ToggleAssetsMenu()
        {
            _showAssetsMenu = !_showAssetsMenu;
        }
    }
}
