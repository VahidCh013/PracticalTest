using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PracticalTest.Domain.Write.Common;

public abstract class EntityBase: Entity<long>
{
  
    protected EntityBase()
    {
    }

    protected EntityBase(long id) : base(id)
    {
    }
}
public static class EntityBaseExtensions
{
    public static void MapKey<TEntityBase>(this EntityTypeBuilder<TEntityBase> builder)
        where TEntityBase:EntityBase
    {
        builder.HasKey( x => x.Id );
    }
}