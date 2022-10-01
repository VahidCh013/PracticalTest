using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PracticalTest.Domain.Write.Blogs;
using PracticalTest.Domain.Write.Common;

namespace PracticalTest.Infrastructure.Blogs.Configurations;

public class BlogPostsConfiguration:IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.MapKey();
        builder.MapTimeAudit();
        builder.Property(x => x.Content).HasMaxLength(5000);
        builder.HasMany(x => x.Comments)
            .WithOne();
    }
}