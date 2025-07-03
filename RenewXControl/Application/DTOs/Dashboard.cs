namespace RenewXControl.Application.DTOs;

public record Dashboard(int TotalAssets, string UserName, IList<string> Role);