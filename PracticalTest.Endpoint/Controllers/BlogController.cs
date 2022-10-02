using System.Security.Claims;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PracticalTest.Domain.Write.Common;
using PracticalTest.Endpoint.Payloads;
using PracticalTest.Infrastructure.Blogs.Commands;

namespace PracticalTest.Endpoint.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController:ControllerBase
{

    private readonly IMediator _mediator;
    private readonly string? _userEmail;

    public BlogController(IMediator mediator)
    {
        _mediator = mediator;
        _userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    [HttpPost("AddBlog")]
    [Authorize]
    
    public async Task<AddBlogPayload> AddBlog(string name, string description,string content,List<string> tags)
    => await _mediator.Send(new AddBlogCommand(name, description,_userEmail,content,tags))
            .Match(r => new AddBlogPayload(r.id, null)
                , e => new AddBlogPayload(null, e.UserErrorFromMessageString()));
    

    [HttpPost("AddComment")]
    [Authorize]
    public async Task<AddCommentPayload> AddComment(string Content, long BlogPostId)
  =>await _mediator.Send(new AddCommentCommand(Content,_userEmail,BlogPostId))
      .Match(r => new AddCommentPayload(r.Id, null)
          , e => new AddCommentPayload(null, e.UserErrorFromMessageString()));


}