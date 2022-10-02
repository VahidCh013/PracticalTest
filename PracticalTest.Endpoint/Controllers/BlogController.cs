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

    public BlogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize]
    public async Task<AddBlogPayload> AddBlog(string name, string description,string content,List<string> tags)
    {
        var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await _mediator.Send(new AddBlogCommand(name, description,userEmail,content,tags))
            .Match(r => new AddBlogPayload(r.id, null)
                , e => new AddBlogPayload(null, e.UserErrorFromMessageString()));
        
        
    }


}