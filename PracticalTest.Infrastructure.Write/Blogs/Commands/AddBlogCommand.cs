using CSharpFunctionalExtensions;
using MediatR;

namespace PracticalTest.Infrastructure.Blogs.Commands;

public record AddBlogCommand (string Name,string Description,string email): IRequest<Result<BlogId>>;

public record BlogId(long id);
