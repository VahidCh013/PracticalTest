using Microsoft.EntityFrameworkCore;
using PracticalTest.Domain.Read.BlogPosts;

namespace PracticalTest.Infrastructure.Read.Repositories;

public class BlogPostRepository:IBlogPostRepository
{
    private readonly IDbContextFactory<PracticalTestReadDbContext> _readDbFactory;

    public BlogPostRepository(IDbContextFactory<PracticalTestReadDbContext> readDbFactory)
    {
        _readDbFactory = readDbFactory;
    }

    public async Task<List<BlogPost>> GetAllBlogPosts(string email)
    {
        var db = _readDbFactory.CreateDbContext();
        return await db.BlogPosts.Include(x=>x.Comments).Where(x => x.UserEmail == email).ToListAsync();
    }

    public async Task<List<BlogPost>> GetTenDaysBlogPosts()
    {
        var db = _readDbFactory.CreateDbContext();
        return await db.BlogPosts.Where(x => x.CreatedOn > DateTimeOffset.Now.AddDays(-10)).ToListAsync();
    }
}