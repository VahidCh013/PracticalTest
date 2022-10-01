using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PracticalTest.Domain.Write.Common;

public interface ITimeAudit
{
    public DateTimeOffset CreatedOn { get; }
    public string CreatedBy { get; }
    public DateTimeOffset ModifiedOn { get; }
    public string ModifiedBy { get; }


}
public static class TimeAuditExtenstion
{
    public static void MapTimeAudit<TEntityBase>(this EntityTypeBuilder<TEntityBase> builder)
        where TEntityBase : EntityBase, ITimeAudit
    {
        builder.Property(x => x.CreatedOn).IsRequired().HasDefaultValueSql("getutcdate()");
        builder.Ignore(x => x.CreatedBy);
        builder.Property(x => x.ModifiedOn).IsRequired().HasDefaultValueSql("getutcdate()");
        builder.Ignore(x => x.ModifiedBy);
    }
}
