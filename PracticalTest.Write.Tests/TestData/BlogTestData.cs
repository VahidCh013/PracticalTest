using PracticalTest.Domain.Write.Blogs;
using PracticalTest.Domain.Write.Users;
using PracticalTest.Infrastructure;
using PracticalTest.Write.TestHelpers;

namespace PracticalTest.Write.Test.TestData;

public static class BlogTestData
{
    
    public static async void SeedUser(this PracticalTestWriteDbContext db)
    {
        var user = User.Create(
            "test.user@gmail.com",
      "test.user@gmail.com",
            "test",
           "StandardUser"
        );
        await db.Users.AddAsync(user.Value);
        await db.SaveChangesAsync();
    }
}