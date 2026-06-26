namespace Application.DTOs.User.Auth;

public record AuthUser
{
    public Guid Id { get; init; }
    public string UserName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;

}