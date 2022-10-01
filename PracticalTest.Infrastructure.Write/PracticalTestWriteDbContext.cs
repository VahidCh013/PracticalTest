using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PracticalTest.Infrastructure;

public class PracticalTestWriteDbContext:IdentityDbContext<IdentityUser>
{
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