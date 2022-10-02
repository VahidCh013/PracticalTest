using PracticalTest.Domain.Write.Common.Mediator;

namespace PracticalTest.Domain.Write.Blogs.Events;

public class BlogPostCreatedEvent:IDomainEvent,ICreatedEvent<BlogPost, BlogPostCreatedEvent>
{
    public long BlogPostId { get; set; }
    public void CreateFromEntity(BlogPost entity)
    {
        BlogPostId = entity.Id;
    }
}