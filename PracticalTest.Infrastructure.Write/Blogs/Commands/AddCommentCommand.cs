using CSharpFunctionalExtensions;
using MediatR;

namespace PracticalTest.Infrastructure.Blogs.Commands;

public record AddCommentCommand(string Content, string Email, long BlogPostId) : IRequest<Result<CommentId>>;


public record CommentId(long Id);