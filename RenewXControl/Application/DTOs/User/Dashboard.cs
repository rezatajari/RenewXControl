namespace RenewXControl.Application.DTOs.User;

public record Dashboard(int TotalAssets, string UserName, IList<string> Role);