using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PracticalTest.Domain.Write.Blogs;
using PracticalTest.Domain.Write.Common;
using PracticalTest.Domain.Write.ValueObjects;

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

        builder.Property(x => x.Name)
            .HasConversion(v => v.Value,
                v => Name.Create(v).Value)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1000);
        builder.HasOne(x => x.User)
            .WithMany(x => x.BlogPosts);
    }
}