using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PracticalTest.Transformations.Destinations.BlogPosts.Configurations;

public class CommentReadModelConfiguration:IEntityTypeConfiguration<CommentReadModel>
{
    public void Configure(EntityTypeBuilder<CommentReadModel> builder)
    {
        builder.ToTable("Comments");
        builder.HasKey(x => x.Id).IsClustered(false);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.BlogPostId).IsRequired();
        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.CreatedOn).IsRequired();
        builder.Property(x => x.CreatedOn).IsRequired();
        builder.Property(x => x.BlogPostName).IsRequired();
    }
}