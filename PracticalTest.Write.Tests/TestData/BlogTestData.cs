using System.Diagnostics.Tracing;
using Microsoft.EntityFrameworkCore;
using PracticalTest.Domain.Write.Blogs;
using PracticalTest.Domain.Write.Users;
using PracticalTest.Domain.Write.ValueObjects;
using PracticalTest.Infrastructure;


namespace PracticalTest.Write.Test.TestData;

public static class BlogTestData
{
    
    public static async void Seed(this PracticalTestWriteDbContext db,bool seedBlog=false)
    {
        var users = new List<User>
        {
            User.Create(
                "test.user@gmail.com",
                "test.user@gmail.com",
                "test",
                "StandardUser"
            ).Value,
            User.Create(
                "test2.user@gmail.com",
                "test2.user@gmail.com",
                "test",
                "StandardUser"
            ).Value
        };
        
       
        var tags = new List<Tag>() { Tag.Create("Angular").Value,Tag.Create("C#").Value };
        await db.Users.AddRangeAsync(users);
        await db.Tags.AddRangeAsync(tags);
        await db.SaveChangesAsync();
        if (seedBlog)
            db.SeedBlog();
        await db.SaveChangesAsync();
    }

    private static async void SeedBlog(this PracticalTestWriteDbContext db)
    {
        var user =await db.Users.FirstAsync();
        var name = Name.Create("TestBlog");
        var blog = BlogPost.Create(name.Value, "description", user, "Content");
        await db.BlogPosts.AddAsync(blog.Value);
        await db.SaveChangesAsync();
    }
}