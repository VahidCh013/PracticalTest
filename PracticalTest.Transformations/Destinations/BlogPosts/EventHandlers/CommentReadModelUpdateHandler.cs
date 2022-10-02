using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticalTest.Domain.Write.Blogs;
using PracticalTest.Domain.Write.Blogs.Events;
using PracticalTest.Domain.Write.Common;
using PracticalTest.Infrastructure;

namespace PracticalTest.Transformations.Destinations.BlogPosts.EventHandlers;

public class CommentReadModelUpdateHandler:INotificationHandler<BlogPostCommentCreatedEvent>
{
    private readonly IDbContextFactory<PracticalTestWriteDbContext> _sourceFactory;
    private readonly IDbContextFactory<PracticalTestTransferDbContext> _destFactory;

    public CommentReadModelUpdateHandler(IDbContextFactory<PracticalTestWriteDbContext> sourceFactory, IDbContextFactory<PracticalTestTransferDbContext> destFactory)
    {
        _sourceFactory = sourceFactory;
        _destFactory = destFactory;
    }

    public async Task Handle(BlogPostCommentCreatedEvent notification, CancellationToken cancellationToken)
    {
        await HandleChange(notification.BlogPostId,cancellationToken);
    }
    
    private async Task HandleChange(long blogPostId, CancellationToken cancellationToken, bool force = false)
    {
        await using var source = _sourceFactory.CreateDbContext();
        var maybeBlogPost = await source.BlogPosts.MaybeFindAsync(blogPostId);
        await UpdateReadData(maybeBlogPost, cancellationToken, force);
    }

    private async Task UpdateReadData(Maybe<BlogPost> maybeBlogPost, CancellationToken cancellationToken,
        bool force = false)
    {
        
    }
}