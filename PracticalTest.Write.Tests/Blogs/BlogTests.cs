using FluentAssertions.CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PracticalTest.Domain.Write.Blogs;
using PracticalTest.Infrastructure;
using PracticalTest.Infrastructure.Blogs.Commands;
using PracticalTest.Write.Test.TestData;
using PracticalTest.Write.TestHelpers;
using Xunit;
using Xunit.Extensions.AssertExtensions;


namespace PracticalTest.Write.Test.Blogs;

public class BlogTests
{
    [Fact]
    public async void Can_Create_Blog()
    {
        var dbFactory = DbContextInitialization.GetInMemoryDbOptionsBuilder<PracticalTestWriteDbContext>().Options;
        var dbContextFactory = DbContextInitialization.GetDbContextFactory(dbFactory);
        var db = dbContextFactory.CreateDbContext();
        db.Seed();
        var name = "Sport";
        var description = "Sport Description";
        var command = new AddBlogCommand(name, description, "test.user@gmail.com","Content",null);
        var handler = new AddBlogCommandHandler(dbContextFactory);
        var result = await handler.Handle(command, CancellationToken.None);
        result.Should().BeSuccess();
        var db2 = dbContextFactory.CreateDbContext();
        var actualBlog = await db2.BlogPosts.FirstAsync();
        actualBlog.ShouldNotBeNull();
        actualBlog.Name.Value.ShouldEqual(name);
    }
    
    [Fact]
    public async void Can_Create_Blog_With_Empty_Name()
    {
        var dbFactory = DbContextInitialization.GetInMemoryDbOptionsBuilder<PracticalTestWriteDbContext>().Options;
        var dbContextFactory = DbContextInitialization.GetDbContextFactory(dbFactory);
        var db = dbContextFactory.CreateDbContext();
        db.Seed();
        var name = "";
        var description = "Sport Description";
        var command = new AddBlogCommand(name, description, "test.user@gmail.com","Content",null);
        var handler = new AddBlogCommandHandler(dbContextFactory);
        var result = await handler.Handle(command, CancellationToken.None);
        result.Should().BeFailure();
    }

    [Fact]
    public async void Blog_Should_Own_A_User()
    {
        var dbFactory = DbContextInitialization.GetInMemoryDbOptionsBuilder<PracticalTestWriteDbContext>().Options;
        var dbContextFactory = DbContextInitialization.GetDbContextFactory(dbFactory);
        var db = dbContextFactory.CreateDbContext();
        db.Seed();
        var name = "Sport";
        var description = "Sport Description";
        var user = "test.user@gmail.com";
        var command = new AddBlogCommand(name, description, user,"Content",null);
        var handler = new AddBlogCommandHandler(dbContextFactory);
        var result = await handler.Handle(command, CancellationToken.None);
        result.Should().BeSuccess();
        var db2 = dbContextFactory.CreateDbContext();
        var actualBlog = await db2.BlogPosts.FirstAsync();
        actualBlog.ShouldNotBeNull();
        actualBlog.User.Email.ShouldEqual(user);
    }

    [Fact]
    public async void Blog_Can_Apply_Tags()
    {
        var dbFactory = DbContextInitialization.GetInMemoryDbOptionsBuilder<PracticalTestWriteDbContext>().Options;
        var dbContextFactory = DbContextInitialization.GetDbContextFactory(dbFactory);
        var db = dbContextFactory.CreateDbContext();
        db.Seed();
        var name = "Sport";
        var description = "Sport Description";
        var user = "test.user@gmail.com";
        var tags = new List<string> { "Angular","C#" };
        var command = new AddBlogCommand(name, description, user,"Content",tags);
        var handler = new AddBlogCommandHandler(dbContextFactory);
        var result = await handler.Handle(command, CancellationToken.None);
        result.Should().BeSuccess();
        var db2 = dbContextFactory.CreateDbContext();
        var actualBlog = await db2.BlogPosts.FirstAsync();
        actualBlog.Tags.Select(x=>x.Name).ShouldEqual(tags);
    }
}