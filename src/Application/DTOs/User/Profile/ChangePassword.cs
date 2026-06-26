namespace Application.DTOs.User.Profile;

public record ChangePassword
{
    public string CurrentPassword { get; init; }=string.Empty;
    public string NewPassword { get; init; }= string.Empty;
    public string ConfirmNewPassword { get; init; } = string.Empty;
}