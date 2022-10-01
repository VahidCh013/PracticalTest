using System.Security.Cryptography;
using System.Text;

using Microsoft.EntityFrameworkCore.Internal;


namespace PracticalTest.Infrastructure.Repositories.UserRepositories;

public class UserRepository
{
    // private readonly DbContextFactory<PracticalTestWriteDbContext> _dbContextFactory;
    //
    // public UserRepository(DbContextFactory<PracticalTestWriteDbContext> dbContextFactory)
    // {
    //     _dbContextFactory = dbContextFactory;
    // }
    //
    // public async Task<User?> FindUserByEmail(string email,string password)
    // {
    //     var dbContext = _dbContextFactory.CreateDbContext();
    //     var hashedPassword = ComputeSha256Hash(password);
    //     return await dbContext.Users.SingleOrDefaultAsync(x => x.Email == email && x.Password==hashedPassword);
    // }
    //
    // private string ComputeSha256Hash(string rawData)
    // {
    //     // Create a SHA256   
    //     using SHA256 sha256Hash = SHA256.Create();
    //     // ComputeHash - returns byte array  
    //     byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));  
    //
    //     // Convert byte array to a string   
    //     var builder = new StringBuilder();  
    //     foreach (var t in bytes)
    //     {
    //         builder.Append(t.ToString("x2"));
    //     }  
    //     return builder.ToString();
    // }  

}