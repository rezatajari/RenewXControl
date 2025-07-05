using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using RXC.Client.DTOs.User;

namespace RXC.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _nav;
        private readonly IJSRuntime _js;

        public AuthService(HttpClient http, NavigationManager nav, IJSRuntime js)
        {
            _http = http;
            _nav = nav;
            _js = js;
        }

        public async Task<bool> LoginAsync(Login model)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", model);
            if (!response.IsSuccessStatusCode)
                return false;

            var token = await response.Content.ReadAsStringAsync();
            await _js.InvokeVoidAsync("localStorage.setItem", "authToken", token);
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _nav.NavigateTo("/dashboard");
            return true;
        }

        public async Task<bool> RegisterAsync(Register model)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", model);
            return response.IsSuccessStatusCode;
        }
    }
}
