using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
            .Ensure(async name=>!await writeDb.Blogs.AnyAsync(x => x.Name == name,  cancellationToken),
                ErrorCode.AlreadyExists.WithMessage($"Blog with the name \"{request.Name}\" already exists."))
            .Map( async n=>new
            {
                Name=n,
                User=await writeDb.Users.FirstAsync(x => x.Email == request.email, cancellationToken: cancellationToken)
            })
            .Map(result => Blog.Create(result.Name, request.Description, result.User))
            .OnSuccessTry(async r =>
            {
                await writeDb.Blogs.AddAsync(r.Value, cancellationToken);
                await writeDb.SaveChangesAsync(cancellationToken);
                return new BlogId(r.Value.Id);
            },ex=>"Error occured");
        
    }
}