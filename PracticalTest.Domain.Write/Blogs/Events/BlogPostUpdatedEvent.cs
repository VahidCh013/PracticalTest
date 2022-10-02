using PracticalTest.Domain.Write.Common.Mediator;

namespace PracticalTest.Domain.Write.Blogs.Events;

public class BlogPostUpdatedEvent:IDomainEvent
{
    public long BlogPostId { get;  }

    public BlogPostUpdatedEvent(long blogPostId)
    {
        BlogPostId = blogPostId;
    }
}