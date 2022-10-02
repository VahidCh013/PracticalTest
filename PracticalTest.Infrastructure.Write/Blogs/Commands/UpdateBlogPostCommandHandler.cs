using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticalTest.Domain.Write.ValueObjects;

namespace PracticalTest.Infrastructure.Blogs.Commands;

public class UpdateBlogPostCommandHandler:IRequestHandler<UpdateBlogPostCommand,Result<UpdateBlogId>>
{
    private readonly IDbContextFactory<PracticalTestWriteDbContext> _writeDbContextFactory;

    public UpdateBlogPostCommandHandler(IDbContextFactory<PracticalTestWriteDbContext> writeDbContextFactory)
    {
        _writeDbContextFactory = writeDbContextFactory;
    }

    public async Task<Result<UpdateBlogId>> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var writeDb = _writeDbContextFactory.CreateDbContext();
        var blogPost=await writeDb.BlogPosts.FirstOrDefaultAsync(x => x.Id == request.BlogPostId,
            cancellationToken: cancellationToken);
        return await Result.FailureIf(blogPost==null,$"No BlogPost found for Id {request.BlogPostId}")
            .Map(()=>blogPost)
            .Ensure(_=>blogPost.User.Email==request.Email,"You are not allowed to update this blog")
            .Bind(_=>Name.Create(request.Name))
            .Check(name=>blogPost.UpdateName(name))
            .Check(_=>blogPost.UpdateDescription(request.Description))
            .Map(async _=>new
            {
                BlogPost=blogPost,
                Tags=await writeDb.Tags.Where(x=>request.Tags.Contains(x.Name)).ToListAsync(cancellationToken: cancellationToken)
            })
            .Check(r=>r.BlogPost.ApplyTags(r.Tags))
            .OnSuccessTry(async result =>
            {
                await writeDb.SaveChangesAsync(cancellationToken);
                return new UpdateBlogId(request.BlogPostId);
            },ex=>"Error occured");
    }
}