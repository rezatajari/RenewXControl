using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using WebClient.DTOs;
using WebClient.DTOs.User.Profile;

namespace WebClient.Pages.Dashboard.Profile;

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

            // Create temporary preview
            var imageFile = await file.RequestImageFileAsync("image/jpeg", 300, 300);
            var buffer = new byte[imageFile.Size];
            await imageFile.OpenReadStream().ReadAsync(buffer);
            _tempImageUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(buffer)}";
            StateHasChanged();

            // Upload the original file (not the resized one)
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 10_000_000));
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

            content.Add(fileContent, "file", file.Name);

            var response = await Http.PostAsync("api/File/Upload/ProfileImage", content);
            var responseContent = await response.Content.ReadFromJsonAsync<GeneralResponse<string>>();

            if (responseContent is null || !responseContent.IsSuccess)
            {
                _errorMessage = responseContent?.Message ?? "Upload failed";
                _tempImageUrl = string.Empty;
                return;
            }

            _editProfile.ProfileImage = responseContent.Data;

            // Don't clear _tempImageUrl here - let it show until page reload
            StateHasChanged();
        }
        catch (Exception ex)
        {
            _errorMessage = $"Error uploading image: {ex.Message}";
            _tempImageUrl = string.Empty;
        }
        finally
        {
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
            {
                _errorMessage = result.Message;
                return;
            }

            await JS.InvokeVoidAsync("localStorage.setItem", "EditProfileSuccess", result.Message);
            Nav.NavigateTo("dashboard/profile", forceLoad: true); // Add forceLoad to ensure fresh data
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