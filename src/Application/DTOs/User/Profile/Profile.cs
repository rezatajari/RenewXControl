namespace Application.DTOs.User.Profile;

public record Profile(int TotalAssets, string UserName, IList<string> Role,string? ProfileImage);
