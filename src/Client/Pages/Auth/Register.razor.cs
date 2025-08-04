using Microsoft.JSInterop;

namespace RXC.Client.Pages.Auth
{
    public partial class Register
    {
        private DTOs.User.Auth.Register model = new();
        private string errorMessage = string.Empty;
        private bool isLoading = false;

        private async Task RegisterUser()
        {
            isLoading = true;
            errorMessage = string.Empty;

            try
            {
                var response = await AuthService.RegisterAsync(model);
                if (!response.IsSuccess)
                {
                    errorMessage = response.Message ?? "Registration failed";
                }
                else
                {
                    await JS.InvokeVoidAsync("localStorage.setItem", "RegisterSuccess", response.Message ?? "Registration successful");
                    Navigation.NavigateTo("/login");
                }
            }
            catch (Exception ex)
            {
                errorMessage = "An error occurred during registration";
                Console.Error.WriteLine(ex.Message);
            }
            finally
            {
                isLoading = false;
            }
        }
    }
}
