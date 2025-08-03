using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using RXC.Client.DTOs;
using RXC.Client.DTOs.User.Profile;

namespace RXC.Client.Pages.Dashboard.Profile;

public partial class Edit
{
    private string _errorMessage = string.Empty;
    private EditProfile _editProfile = new();
    private bool _isLoading = false;
    private async Task OnInputFileChange(InputFileChangeEventArgs e)
    {
        try
        {
            var file = e.File;
            _errorMessage = string.Empty;

            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 10_000_000)); // e.g., 10 MB limit
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

            content.Add(fileContent, "file", file.Name);

            var response = await Http.PostAsync("api/File/Upload/ProfileImage", content);
            var responseContent = await response.Content.ReadFromJsonAsync<GeneralResponse<string>>();

            if (responseContent is null || !responseContent.IsSuccess)
            {
                _errorMessage = responseContent?.Message ?? "Upload failed";
                return;
            }

            _editProfile.ProfileImage = responseContent.Data;

        }
        catch (Exception ex)
        {
            _errorMessage = $"Error uploading image: {ex.Message}";
        }
    }


    private async Task HandleValidSubmit()
    {
        _isLoading = true;
        _errorMessage = string.Empty;
        try
        {
            var result = await AuthService.EditProfileAsync(_editProfile);
            if (!result.IsSuccess)
                _errorMessage = result.Message;

            await JS.InvokeVoidAsync("localStorage.setItem", "EditProfileSuccess", result.Message);
            Nav.NavigateTo("dashboard/profile");
        }
        catch (Exception ex)
        {
            _errorMessage = $"Error updating profile: {ex.Message}";
        }
        finally
        {
            _isLoading = false;
        }
    }
}