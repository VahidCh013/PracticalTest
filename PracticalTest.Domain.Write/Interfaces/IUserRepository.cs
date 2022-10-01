using PracticalTest.Domain.Write.Users;

namespace PracticalTest.Domain.Write.Interfaces;

public interface IUserRepository
{
    Task<User> FindUserByEmail(string email,string password);
 
}