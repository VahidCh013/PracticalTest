using System.Security.Claims;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticalTest.Domain.Read.BlogPosts;
using PracticalTest.Domain.Write.Common;
using PracticalTest.Endpoint.Errors;
using PracticalTest.Endpoint.Payloads;
using PracticalTest.Infrastructure.Blogs.Commands;

namespace PracticalTest.Endpoint.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IBlogPostRepository _blogPostRepository;

    public BlogController(IMediator mediator, IBlogPostRepository blogPostRepository)
    {
        _mediator = mediator;
        _blogPostRepository = blogPostRepository;
    }

    [HttpPost("AddBlog")]
    [Authorize]
    public async Task<AddBlogPayload> AddBlog(string name, string description, string content, string[] tags)
    {
        var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await _mediator.Send(new AddBlogCommand(name, description, userEmail, content, tags.ToList()))
            .Match(r => new AddBlogPayload(r.id, null)
                , e => new AddBlogPayload(null, e.UserErrorFromMessageString()));
    }

    [HttpPost("UpdateBlogPost")]
    [Authorize]
    public async Task<UpdateBlogPostPayload> UpdateBlogPost(long blogPostId, string name, string description,
        string content, string[] tags)
    {
        var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await _mediator
            .Send(new UpdateBlogPostCommand(blogPostId, name, description, userEmail, content, tags.ToList()))
            .Match(r => new UpdateBlogPostPayload(r.Id, null)
                , e => new UpdateBlogPostPayload(null, e.UserErrorFromMessageString()));
    }

    [HttpPost("AddComment")]
    [Authorize]
    public async Task<AddCommentPayload> AddComment(string content, long blogPostId)
    {
        var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await _mediator.Send(new AddCommentCommand(content, userEmail, blogPostId))
            .Match(r => new AddCommentPayload(r.Id, null)
                , e => new AddCommentPayload(null, e.UserErrorFromMessageString()));
    }

    [HttpGet("GetAllOwnBlogPosts")]
   
    public async Task<List<BlogPost>> GetAllOwnBlogPosts()
    {
        var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var allOwnBlogPosts=await _blogPostRepository.GetAllBlogPosts(userEmail);
        if (allOwnBlogPosts.Count == 0)
        {
            throw new NotFoundCustomException("no records found");
        }

        return allOwnBlogPosts;

    }
    [HttpGet("GetLatestTenDaysBlogPosts")]
    [Authorize]
    public async Task<List<BlogPost>> GetLatestTenDaysBlogPosts()
    {
        var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await _blogPostRepository.GetTenDaysBlogPosts();
    }
}