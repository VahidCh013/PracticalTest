using FluentAssertions.CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
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
        db.SeedUser();
        var name = "Sport";
        var description = "Sport Description";
        var command = new AddBlogCommand(name, description, "test.user@gmail.com");
        var handler = new AddBlogCommandHandler(dbContextFactory);
        var result = await handler.Handle(command, CancellationToken.None);
        result.Should().BeSuccess();
        var actualBlog = await db.Blogs.FirstAsync();
        actualBlog.ShouldNotBeNull();
        actualBlog.Name.Value.ShouldEqual(name);
    }
    
    [Fact]
    public async void Can_Create_Blog_With_Emapty_Name()
    {
        var dbFactory = DbContextInitialization.GetInMemoryDbOptionsBuilder<PracticalTestWriteDbContext>().Options;
        var dbContextFactory = DbContextInitialization.GetDbContextFactory(dbFactory);
        var db = dbContextFactory.CreateDbContext();
        db.SeedUser();
        var name = "";
        var description = "Sport Description";
        var command = new AddBlogCommand(name, description, "test.user@gmail.com");
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
        db.SeedUser();
        var name = "Sport";
        var description = "Sport Description";
        var user = "test.user@gmail.com";
        var command = new AddBlogCommand(name, description, user);
        var handler = new AddBlogCommandHandler(dbContextFactory);
        var result = await handler.Handle(command, CancellationToken.None);
        result.Should().BeSuccess();
        var actualBlog = await db.Blogs.FirstAsync();
        actualBlog.ShouldNotBeNull();
        actualBlog.User.Email.ShouldEqual(user);
    }
}