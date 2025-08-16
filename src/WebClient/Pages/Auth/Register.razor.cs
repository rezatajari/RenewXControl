using Microsoft.JSInterop;

namespace WebClient.Pages.Auth;

public partial class Register
{
    private readonly DTOs.User.Auth.Register _model=new();
    private string _errorMessage = string.Empty;
    private bool _isLoading = false;

    private async Task RegisterUser()
    {
        _isLoading = true;
        _errorMessage = string.Empty;

        try
        {
            var response = await AuthService.RegisterAsync(_model);
            if (!response.IsSuccess)
            {
                _errorMessage = response.Message ?? "Registration failed";
            }
            else
            {
                await Js.InvokeVoidAsync("localStorage.setItem", "RegisterSuccess", response.Message ?? "Registration successful");
                Navigation.NavigateTo("/login");
            }
        }
        catch (Exception ex)
        {
            _errorMessage = "An error occurred during registration";
        }
        finally
        {
            _isLoading = false;
        }
    }
}