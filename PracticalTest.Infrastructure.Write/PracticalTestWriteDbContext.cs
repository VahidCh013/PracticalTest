using System.Reflection;
using Microsoft.EntityFrameworkCore;

using PracticalTest.Domain.Write.Blogs;

using PracticalTest.Domain.Write.Users;


namespace PracticalTest.Infrastructure;

public class PracticalTestWriteDbContext:DbContext
{
    
    public DbSet<User> Users { get; set; }
    public DbSet<BlogPost> BlogPosts { get; set; }
    
    public DbSet<Tag> Tags { get; set; }
    public PracticalTestWriteDbContext(DbContextOptions<PracticalTestWriteDbContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("dbo");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(PracticalTestWriteDbContext)) ??
                                                throw new
                                                    InvalidOperationException(
                                                        $"Could not apply EF configurations because no assembly was found for type {nameof(PracticalTestWriteDbContext)}."));
        base.OnModelCreating(builder);
    }
}