using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using RXC.Client.DTOs;
using RXC.Client.DTOs.User.Auth;

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

        public async Task<GeneralResponse<string>> LoginAsync(Login model)
        {
            var result = await _http.PostAsJsonAsync(requestUri:"api/auth/login", model);
            var response = await result.Content.ReadFromJsonAsync<GeneralResponse<string>>();

            if (!result.IsSuccessStatusCode)
                return response;

            var token = response.Data;
            await _js.InvokeVoidAsync(identifier:"localStorage.setItem", "authToken", token);
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme:"Bearer", token);
            _nav.NavigateTo(uri:"/dashboard/profile");
            return response;
        }

        public async Task<GeneralResponse<string>> RegisterAsync(Register model)
        {
            var result= await _http.PostAsJsonAsync(requestUri:"api/auth/register", model);
            return await  result.Content.ReadFromJsonAsync<GeneralResponse<string>>();
        }
    }
}
