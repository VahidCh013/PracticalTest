using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticalTest.Domain.Write.Blogs;


namespace PracticalTest.Infrastructure.Blogs.Commands;

public class AddCommentCommandHandler:IRequestHandler<AddCommentCommand,Result<CommentId>>
{
    private readonly IDbContextFactory<PracticalTestWriteDbContext> _writeDbContextFactory;

    public AddCommentCommandHandler(IDbContextFactory<PracticalTestWriteDbContext> writeDbContextFactory)
    {
        _writeDbContextFactory = writeDbContextFactory;
    }

    public async Task<Result<CommentId>> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var writeDb = _writeDbContextFactory.CreateDbContext();
        return await Maybe.From(await writeDb.BlogPosts.FirstOrDefaultAsync(x=>x.Id==request.BlogPostId, cancellationToken: cancellationToken)
            ).ToResult($"No BlogPost found for Id {request.BlogPostId}")
            .Map(async blogPost=>new
            {
                BlogPost=blogPost,
                User=await writeDb.Users.FirstAsync(x => x.Email == request.Email, cancellationToken: cancellationToken)
            })
            .Check(result=>Comment.Create(request.Content,result.User))
            .Check(result=>result.BlogPost.AddComment(Comment.Create(request.Content,result.User).Value,result.User))
            .OnSuccessTry(async result =>
            {
                await writeDb.SaveChangesAsync(cancellationToken);
                return new CommentId(result.BlogPost.Id);
            },ex=>"Error occured");
    }
}