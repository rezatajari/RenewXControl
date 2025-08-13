using Microsoft.JSInterop;

namespace WebClient.Pages.Dashboard.Profile;

public partial class ChangePassword
{
    private DTOs.User.Auth.ChangePassword model = new();
    private bool _isLoading = false;
    private string _errorMessage = string.Empty;

    private async Task ChangePass()
    {
        _isLoading = true;
        _errorMessage = null;

        try
        {
            var result = await AuthService.ChangePasswordAsync(model);

            if (!result.IsSuccess)
            {
                _errorMessage = result.Message;
            }
            else
            {
                await JS.InvokeVoidAsync(identifier: "localStorage.setItem", "passwordChangeSuccess", $"{result.Message}");
                Nav.NavigateTo(uri: "/Dashboard/profile");
            }
        }
        catch (Exception ex)
        {
            _errorMessage = "An error occurred while changing password";
        }
        finally
        {
            _isLoading = false;
        }

    }
}