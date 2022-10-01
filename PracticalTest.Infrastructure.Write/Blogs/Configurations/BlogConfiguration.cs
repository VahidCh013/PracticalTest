using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using PracticalTest.Domain.Write.Blogs;
using PracticalTest.Domain.Write.Common;
using PracticalTest.Domain.Write.ValueObjects;

namespace PracticalTest.Infrastructure.Blogs.Configurations;

public class BlogConfiguration:IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
    
        builder.MapKey();
        builder.MapTimeAudit();
        builder.Property(x => x.Name)
            .HasConversion(v => v.Value,
                v => Name.Create(v).Value)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1000);
        builder.HasOne(x => x.User)
            .WithMany(x => x.Blogs);

    }
}