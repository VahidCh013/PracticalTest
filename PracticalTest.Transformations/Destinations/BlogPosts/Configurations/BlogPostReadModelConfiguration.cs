using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PracticalTest.Transformations.Destinations.BlogPosts.Configurations;

public class BlogPostReadModelConfiguration:IEntityTypeConfiguration<BlogPostReadModel>
{
    public void Configure(EntityTypeBuilder<BlogPostReadModel> builder)
    {
        builder.ToTable("BlogPosts");
        builder.HasKey(x => x.Id).IsClustered(false);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Desciption).IsRequired();
        builder.Property(x => x.UserEmail).IsRequired();
        builder.Property(x => x.Content).IsRequired();
        builder.Property(x => x.CreatedOn).IsRequired();
        builder.Property(x => x.CreatedOn).IsRequired();
        builder.Property(x => x.Tags);

        builder.HasMany(x => x.CommentReadModels)
            .WithOne()
            .HasForeignKey(x => x.BlogPostId);

    }
}