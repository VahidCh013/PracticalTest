using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using PracticalTest.Domain.Write.Users;

namespace PracticalTest.Endpoint.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController:ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _config;
    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration config)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _config = config;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserDetail? userDetail)
    {
        if (!ModelState.IsValid || userDetail == null)
        {
            return new BadRequestObjectResult(new { Message = "User Registration Failed" });
        }
        var identityUser = new IdentityUser { UserName = userDetail.UserName, Email = userDetail.Email };
        var result = await _userManager.CreateAsync(identityUser, userDetail.Password);
        if (!result.Succeeded)
        {
            var dictionary = new ModelStateDictionary();
            foreach (IdentityError error in result.Errors)
            {
                dictionary.AddModelError(error.Code, error.Description);
            }

            return new BadRequestObjectResult(new { Message = "User Registration Failed", Errors = dictionary });
        }
        return Ok(new { Message = "User registration Successful" });
    }

    [HttpPost]
    [Route("AccessToken")]
    public async Task<IActionResult> AccessToken([FromBody] LoginCredential? credential)
    {
        if (!ModelState.IsValid || credential == null)
        {
            return new BadRequestObjectResult(new { Message = "Login failed" });
        }

        var identityUser = await _userManager.FindByEmailAsync(credential.Email);
        var roles = await _userManager.GetRolesAsync(identityUser);
        if (identityUser == null)
        {
            return new BadRequestObjectResult(new { Message = "Login failed" });
        }

        var result =
            _userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash,
                credential.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            return new BadRequestObjectResult(new { Message = "Login failed" });
        }
        var accessToken = GenerateToken(credential, roles.ToList());
        return Ok(new { AccessToken = accessToken });
    }

    private string GenerateToken(LoginCredential user,List<string> roles)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>()
        {
            new (ClaimTypes.Email,user.Email)
        };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

