using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PracticalTest.Domain.Read.Users;

namespace PracticalTest.Infrastructure.Read;

public class PracticalTestReadDbContext:DbContext
{
    public DbSet<User> Users { get; set; }
    public PracticalTestReadDbContext(DbContextOptions<PracticalTestReadDbContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("read");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(PracticalTestReadDbContext)) ??
                                                throw new
                                                    InvalidOperationException(
                                                        $"Could not apply EF configurations because no assembly was found for type {nameof(PracticalTestReadDbContext)}."));
        base.OnModelCreating(builder);
    }
    #region SaveChanges

    public override int SaveChanges()
    {
        throw new InvalidOperationException("User Pdb.Api.Domain.Write.PdbWriteContext instead.");
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        throw new InvalidOperationException("User Pdb.Api.Domain.Write.PdbWriteContext instead.");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        throw new InvalidOperationException("User Pdb.Api.Domain.Write.PdbWriteContext instead.");
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        throw new InvalidOperationException("User Pdb.Api.Domain.Write.PdbWriteContext instead.");
    }

    #endregion
}