using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PracticalTest.Domain.Write.Blogs;
using PracticalTest.Domain.Write.Common;

namespace PracticalTest.Infrastructure.Blogs.Configurations;

public class CommentConfiguration:IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");
        builder.MapKey();
        builder.MapTimeAudit();
        builder.Property(x => x.Content).HasMaxLength(2000);
        builder.HasOne(x => x.User);
    }
}