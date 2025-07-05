namespace RenewXControl.Application.DTOs.User;

public record Profile(int TotalAssets, string UserName, IList<string> Role);