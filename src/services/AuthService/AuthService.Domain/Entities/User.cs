namespace AuthService.Domain.Entities;

public class User
{
    public required Guid UserId { get; init; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PasswordSalt { get; set; }
    public string PasswordHash { get; set; }
}