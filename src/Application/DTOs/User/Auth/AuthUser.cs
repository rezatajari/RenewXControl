namespace Application.DTOs.User.Auth;

public record AuthUser
{
    public Guid Id { get; init; }
    public string? UserName { get; init; } 
    public string Email { get; init; } = string.Empty;

}