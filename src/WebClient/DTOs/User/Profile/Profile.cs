namespace WebClient.DTOs.User.Profile;

public record Profile(int TotalAssets, string UserName, string? ProfileImage, IList<string> Role);