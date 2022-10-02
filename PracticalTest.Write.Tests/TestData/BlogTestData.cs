using System.Diagnostics.Tracing;
using PracticalTest.Domain.Write.Blogs;
using PracticalTest.Domain.Write.Users;
using PracticalTest.Infrastructure;
using PracticalTest.Write.TestHelpers;

namespace PracticalTest.Write.Test.TestData;

public static class BlogTestData
{
    
    public static async void Seed(this PracticalTestWriteDbContext db)
    {
        var user = User.Create(
            "test.user@gmail.com",
      "test.user@gmail.com",
            "test",
           "StandardUser"
        );
        var tags = new List<Tag>() { Tag.Create("Angular").Value,Tag.Create("C#").Value };
        await db.Users.AddAsync(user.Value);
        await db.Tags.AddRangeAsync(tags);
        await db.SaveChangesAsync();
    }
}