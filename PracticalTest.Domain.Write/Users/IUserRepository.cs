namespace PracticalTest.Domain.Write.Users;

public interface IUserRepository
{
    Task<User> FindUserByEmail(string email, string password);
}