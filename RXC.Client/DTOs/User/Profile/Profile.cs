namespace RXC.Client.DTOs.User.Profile;

public record Profile(int TotalAssets, string UserName, IList<string> Role);