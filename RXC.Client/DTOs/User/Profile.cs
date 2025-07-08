namespace RXC.Client.DTOs.User;

public record Profile(int TotalAssets, string UserName, IList<string> Role);