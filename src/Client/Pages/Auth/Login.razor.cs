using Microsoft.JSInterop;

namespace Client.Pages.Auth;

public partial class Login
{
    private RXC.Client.DTOs.User.Auth.Login model = new();
    private string? errorMessage;
    private string? successMessage;
    private bool isLoading = false;


    protected override async Task OnInitializedAsync()
    {
        successMessage = await JS.InvokeAsync<string>(identifier: "localStorage.getItem", "RegisterSuccess");
        if (!string.IsNullOrEmpty(successMessage))
        {
            await JS.InvokeVoidAsync(identifier: "localStorage.removeItem", "RegisterSuccess");
        }
    }

    private async Task LoginUser()
    {

        successMessage = null;

        isLoading = true;
        errorMessage = null;
        try
        {
            var result = await AuthService.LoginAsync(model);
            if (!result.IsSuccess)
            {
                errorMessage = result.Message;
            }
            else
            {
                await JS.InvokeVoidAsync(identifier: "localStorage.setItem", "loginSuccess", $"{result.Message}");
                Navigation.NavigateTo(uri: "/Dashboard/profile");
            }
        }
        finally
        {
            isLoading = false;
        }
    }
}