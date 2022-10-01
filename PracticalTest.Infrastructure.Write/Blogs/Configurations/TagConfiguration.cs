using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PracticalTest.Domain.Write.Blogs;
using PracticalTest.Domain.Write.Common;

namespace PracticalTest.Infrastructure.Blogs.Configurations;

public class TagConfiguration:IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.MapKey();
        builder.MapTimeAudit();

        builder.HasMany(x => x.BlogPosts)
            .WithMany(x => x.Tags);
    }
}