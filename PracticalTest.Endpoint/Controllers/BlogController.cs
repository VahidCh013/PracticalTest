using System.Security.Claims;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PracticalTest.Domain.Read.BlogPosts;
using PracticalTest.Domain.Write.Common;
using PracticalTest.Domain.Write.Users;
using PracticalTest.Endpoint.Common.Errors;
using PracticalTest.Endpoint.Payloads;
using PracticalTest.Endpoint.Validations;
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
    public async Task<IActionResult> AddComment(CreateCommentRequest commentRequest,[FromServices] CreateCommentRequestValidator validator)
    {
        var result = await validator.ValidateAsync(commentRequest);
        if (!result.IsValid)
        {
            var modelState = new ModelStateDictionary();
            foreach (var failure in result.Errors)
            {
                modelState.AddModelError(failure.PropertyName,failure.ErrorMessage);
            }
            return ValidationProblem(modelState);
        }
        var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await _mediator.Send(new AddCommentCommand(commentRequest.Comment, userEmail, commentRequest.BlogpostId))
            .Match(r => new AddCommentPayload(r.Id, null)
                , e => new AddCommentPayload(null, e.UserErrorFromMessageString()));
        return Ok();
    }

    [HttpGet("GetAllOwnBlogPosts")]
   
    public async Task<List<BlogPost>> GetAllOwnBlogPosts()
    {
        var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var allOwnBlogPosts=await _blogPostRepository.GetAllBlogPosts(userEmail);
        if (allOwnBlogPosts.Count == 0)
        {
            throw new Exception("no records found");
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