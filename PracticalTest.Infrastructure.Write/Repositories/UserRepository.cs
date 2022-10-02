using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PracticalTest.Domain.Write.Users;


namespace PracticalTest.Infrastructure.Repositories;

public class UserRepository:IUserRepository
{
    private readonly DbContextFactory<PracticalTestWriteDbContext> _writeContextFactory;

    public UserRepository(DbContextFactory<PracticalTestWriteDbContext> writeContextFactory)
    {
        _writeContextFactory = writeContextFactory;
    }

    public async Task<User> FindUserByEmail(string email, string password)
    {
        var writeContext = _writeContextFactory.CreateDbContext();
        var hashedPassword = ComputeSha256Hash(password);
        return await writeContext.Users.SingleOrDefaultAsync(x => x.Email == email && x.Password==hashedPassword);
    }
    
    private string ComputeSha256Hash(string rawData)
    {
        // Create a SHA256   
        using SHA256 sha256Hash = SHA256.Create();
        // ComputeHash - returns byte array  
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));  
  
        // Convert byte array to a string   
        var builder = new StringBuilder();  
        foreach (var t in bytes)
        {
            builder.Append(t.ToString("x2"));
        }  
        return builder.ToString();
    } 
}


