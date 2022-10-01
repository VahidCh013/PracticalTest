namespace PracticalTest.Domain.Write.Users;

/// <summary>
/// Anemic model, this model does not have any complex use case.
/// </summary>
public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}

