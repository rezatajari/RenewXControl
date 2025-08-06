using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using RXC.Client.DTOs;
using RXC.Client.DTOs.User.Profile;

namespace Client.Pages.Dashboard.Profile;

public partial class Edit
{
    private string _errorMessage = string.Empty;

    private EditProfile _editProfile = new EditProfile
    {
        UserName = string.Empty,
        ProfileImage = string.Empty
    };

    private bool _isLoading = false;
    private string _tempImageUrl = string.Empty;

    private async Task OnInputFileChange(InputFileChangeEventArgs e)
    {
        _errorMessage = string.Empty;

        try
        {
            var file = e.File;
            
            // 1. Create temporary preview (before upload completes)
            var imageFile = await file.RequestImageFileAsync("image/jpeg", 300, 300);
            var buffer = new byte[imageFile.Size];
            await imageFile.OpenReadStream().ReadAsync(buffer);
            _tempImageUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(buffer)}";
            // Force UI update to show temporary preview
            StateHasChanged();


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
            StateHasChanged();

            // Clear temporary preview
            _tempImageUrl = string.Empty;

        }
        catch (Exception ex)
        {
            _errorMessage = $"Error uploading image: {ex.Message}";
            _tempImageUrl = string.Empty;
            StateHasChanged();
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