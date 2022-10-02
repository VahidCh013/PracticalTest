using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PracticalTest.Domain.Read.BlogPosts;

namespace PracticalTest.Infrastructure.Read.Configurations.BlogPosts;

public class BlogPostConfiguration:IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.ToTable("BlogPosts");
        builder.HasKey(x => x.Id).IsClustered(false);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        builder.Property(x => x.Name);
        builder.Property(x => x.Desciption);
        builder.Property(x => x.UserEmail);
        builder.Property(x => x.Content);
        builder.Property(x => x.CreatedOn);
        builder.Property(x => x.CreatedOn);
        builder.Property(x => x.Tags);

        builder.HasMany(x => x.Comments)
            .WithOne()
            .HasForeignKey(x => x.BlogPostId);
        
    }
}