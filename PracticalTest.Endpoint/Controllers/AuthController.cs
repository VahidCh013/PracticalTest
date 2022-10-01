using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PracticalTest.Domain.Write.Interfaces;
using PracticalTest.Domain.Write.Users;

namespace PracticalTest.Endpoint.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController:ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;
    public AuthController(IConfiguration config, IUserRepository userRepository)
    {
        _config = config;
        _userRepository = userRepository;
    }

    [HttpPost]
    [Route("AccessToken")]
    public async Task<IActionResult> AccessToken([FromBody] LoginCredential? credential)
    {
        var user = await _userRepository.FindUserByEmail(credential.Email,credential.Password);
        if (user is null)
        {
            return new BadRequestObjectResult(new { Message = "Login Failed" });
        }
        var accessToken = GenerateToken(user);
        return Ok(new { AccessToken = accessToken });
    }
    
    
    
    private string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>()
        {
            new (ClaimTypes.Email,user.Email),
            new (ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

