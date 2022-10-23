using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PracticalTest.Endpoint.Controllers;

[Route("[controller]")]
public class ErrorsController:ControllerBase
{
    [HttpGet("/Error")]
    public IActionResult Error()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;
        return Problem(title:exception?.Message,statusCode:400);
    }
}