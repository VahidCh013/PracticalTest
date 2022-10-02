using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PracticalTest.Domain.Read.BlogPosts;

namespace PracticalTest.Infrastructure.Read.Configurations.BlogPosts;

public class CommentConfiguration:IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");
        builder.HasKey(x => x.Id).IsClustered(false);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        builder.Property(x => x.Content);
        builder.Property(x => x.Email);
        builder.Property(x => x.BlogPostId);
        builder.Property(x => x.Content);
        builder.Property(x => x.CreatedOn);
        builder.Property(x => x.CreatedOn);
        builder.Property(x => x.BlogPostName);
    }
}