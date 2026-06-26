namespace Application.DTOs.User.Profile;

public record EditProfile
{
    public string UserName { get; init; }=string.Empty;
    public string? ProfileImage { get; set; }
}