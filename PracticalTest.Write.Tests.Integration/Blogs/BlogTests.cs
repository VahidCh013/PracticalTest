using FluentAssertions.CSharpFunctionalExtensions;
using PracticalTest.Infrastructure;
using PracticalTest.Infrastructure.Blogs.Commands;
using Xunit;

namespace PracticalTest.Write.Tests.Integration.Blogs;

// public class BlogTests:IClassFixture<MsSqlFixture>
// {
//     /// <summary>
//     /// As a user running application can create new Blogs\Posts 
//     /// </summary>
//     [Fact]
//     
//     public async void Can_Create_Blog()
//     {
//         var dbFactory = DbContextInitialization.GetInMemoryDbOptionsBuilder<PracticalTestWriteDbContext>().Options;
//         var dbContextFactory = DbContextInitialization.GetDbContextFactory(dbFactory);
//         var db = dbContextFactory.CreateDbContext();
//         db.Seed();
//         var name = "Sport";
//         var description = "Sport Description";
//         var command = new AddBlogCommand(name, description, "test.user@gmail.com","Content",null);
//         var handler = new AddBlogCommandHandler(dbContextFactory);
//         var result = await handler.Handle(command, CancellationToken.None);
//         result.Should().BeSuccess();
//         var db2 = dbContextFactory.CreateDbContext();
//         var actualBlog = await db2.BlogPosts.FirstAsync();
//         actualBlog.ShouldNotBeNull();
//         actualBlog.Name.Value.ShouldEqual(name);
//     }
// }