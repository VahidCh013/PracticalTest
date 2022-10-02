using CSharpFunctionalExtensions;
using MediatR;


namespace PracticalTest.Infrastructure.Blogs.Commands;

public record UpdateBlogPostCommand(long BlogPostId,string Name,string Description,string Email,string Content,List<string> Tags):IRequest<Result<UpdateBlogId>>;

public record UpdateBlogId(long Id);