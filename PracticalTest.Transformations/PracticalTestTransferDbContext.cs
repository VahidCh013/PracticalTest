﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace PracticalTest.Transformations;

public class PracticalTestTransferDbContext:DbContext
{
    public PracticalTestTransferDbContext(DbContextOptions<PracticalTestTransferDbContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("read");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(PracticalTestTransferDbContext)) ??
                                                throw new
                                                    InvalidOperationException(
                                                        $"Could not apply EF configurations because no assembly was found for type {nameof(PracticalTestTransferDbContext)}."));
        base.OnModelCreating(builder);
    }

}