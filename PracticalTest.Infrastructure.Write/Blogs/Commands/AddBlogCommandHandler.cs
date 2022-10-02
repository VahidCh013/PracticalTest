using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PracticalTest.Domain.Write.Blogs;
using PracticalTest.Domain.Write.Common;
using PracticalTest.Domain.Write.ValueObjects;

namespace PracticalTest.Infrastructure.Blogs.Commands;

public class AddBlogCommandHandler:IRequestHandler<AddBlogCommand,Result<BlogId>>
{
    private readonly IDbContextFactory<PracticalTestWriteDbContext> _writeDbContextFactory;

    public AddBlogCommandHandler(IDbContextFactory<PracticalTestWriteDbContext> writeDbContextFactory)
    {
        _writeDbContextFactory = writeDbContextFactory;
    }

    public async Task<Result<BlogId>> Handle(AddBlogCommand request, CancellationToken cancellationToken)
    {
        var writeDb = _writeDbContextFactory.CreateDbContext();
       
        return await Name.Create(request.Name)
            .Ensure(async name=>!await writeDb.BlogPosts.AnyAsync(x => x.Name == name,  cancellationToken),
                ErrorCode.AlreadyExists.WithMessage($"Blog with the name \"{request.Name}\" already exists."))
            .Map( async n=>new
            {
                Name=n,
                User=await writeDb.Users.FirstAsync(x => x.Email == request.email, cancellationToken: cancellationToken)
            })
            .Map(result => BlogPost.Create(result.Name, request.Description, result.User,request.content))
            .Map(async r=>new
            {
                BlogPost=r.Value,
                Tags=await writeDb.Tags.Where(x=>request.tags.Contains(x.Name)).ToListAsync(cancellationToken: cancellationToken)
            })
            .Check(r=>r.BlogPost.ApplyTags(r.Tags))
            .OnSuccessTry(async r =>
            {
                await writeDb.BlogPosts.AddAsync(r.BlogPost, cancellationToken);
                await writeDb.SaveChangesAsync(cancellationToken);
                return new BlogId(r.BlogPost.Id);
            },ex=>"Error occured");
        
    }
}