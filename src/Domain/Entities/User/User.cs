namespace Domain.Entities.User;

public sealed class User
{
    public Guid Id { get; private set; }

    private User(){}
    private User(string username, string email)
    {
        Id = Guid.NewGuid();
    }

    public static User Create(string username,string email)
    {
        return new User(username,email);
    }
}