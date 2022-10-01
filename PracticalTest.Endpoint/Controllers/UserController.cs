using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PracticalTest.Endpoint.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    //For admin Only
    [HttpGet]
    [Route("Admins")]
    [Authorize(Roles = "StandardUser")]
    public IActionResult AdminEndPoint()
    {
        var currentUser = GetCurrentUser();
        return Ok($"Hi you are an {currentUser.Role}");
    }

    private UserModel GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity != null)
        {
            var userClaims = identity.Claims;
            return new UserModel
            {
                UserName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                Role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value
            };
        }

        return null;
    }
}

public class UserModel
{
    public string UserName { get; set; }
    public string Role { get; set; }
}