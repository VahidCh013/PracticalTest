using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticalTest.Domain.Write.Blogs;
using PracticalTest.Domain.Write.Blogs.Events;
using PracticalTest.Domain.Write.Common;
using PracticalTest.Infrastructure;

namespace PracticalTest.Transformations.Destinations.BlogPosts.EventHandlers;

public class BlogPostReadModelUpdateHandler:INotificationHandler<BlogPostCreatedEvent>,
    INotificationHandler<BlogPostCommentCreatedEvent>,
    INotificationHandler<BlogPostUpdatedEvent>
{
    private readonly IDbContextFactory<PracticalTestWriteDbContext> _sourceFactory;
    private readonly IDbContextFactory<PracticalTestTransferDbContext> _destFactory;

    public BlogPostReadModelUpdateHandler(IDbContextFactory<PracticalTestWriteDbContext> sourceFactory, IDbContextFactory<PracticalTestTransferDbContext> destFactory)
    {
        _sourceFactory = sourceFactory;
        _destFactory = destFactory;
    }
    public async Task Handle(BlogPostCreatedEvent notification, CancellationToken cancellationToken)
    {
        await HandleChange(notification.BlogPostId,cancellationToken);
    }
    
    public async Task Handle(BlogPostCommentCreatedEvent notification, CancellationToken cancellationToken)
    {
        await HandleChange(notification.BlogPostId,cancellationToken);
    }
    public async Task Handle(BlogPostUpdatedEvent notification, CancellationToken cancellationToken)
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
        await using var dest = _destFactory.CreateDbContext();
        Maybe<BlogPostReadModel> mayBeReadModel;
        var blogPost = maybeBlogPost.Value;
        
        mayBeReadModel = await dest.BlogPostReadModels.MaybeFindAsync(blogPost.Id);
        var readModel = mayBeReadModel.Unwrap(new BlogPostReadModel());
        if (mayBeReadModel.HasNoValue)
            await dest.BlogPostReadModels.AddAsync(readModel, cancellationToken);
        readModel.Id = blogPost.Id;
        readModel.Content = blogPost.Content;
        readModel.Desciption = blogPost.Description;
        readModel.Name = blogPost.Name.Value;
        readModel.Tags = string.Join(", ", blogPost.Tags.Select(x=>x.Name).ToArray());
        readModel.UserEmail = blogPost.User.Email;
        readModel.CreatedOn = blogPost.CreatedOn;
        readModel.ModifiedOn = blogPost.ModifiedOn;
        var commentReadModels =await dest.BlogPostReadModels.Include(x => x.CommentReadModels)
            .SelectMany(x=>x.CommentReadModels)
            .Where(x => x.BlogPostId == blogPost.Id).ToListAsync(cancellationToken: cancellationToken);
        dest.CommentReadModels.RemoveRange(commentReadModels);
        var updatedCommentReadModels = new List<CommentReadModel>();
        await dest.SaveChangesAsync(cancellationToken);
        foreach (var comment in blogPost.Comments)
        {
            var commentReadModel = new CommentReadModel()
            {
                Id = comment.Id,
                BlogPostId = blogPost.Id,
                Content = comment.Content,
                Email = comment.User.Email,
                ModifiedOn = comment.ModifiedOn,
                CreatedOn = comment.CreatedOn,
                BlogPostName = blogPost.Name.Value
            };

           updatedCommentReadModels.Add(commentReadModel);
        }
        dest.CommentReadModels.AddRange(updatedCommentReadModels);
        await dest.SaveChangesAsync(cancellationToken);
    }



}