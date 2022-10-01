namespace PracticalTest.Domain.Read.Users;

public interface IUserRepository
{
    Task<User> FindUserByEmail(string email, string password);
}