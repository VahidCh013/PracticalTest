﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PracticalTest.Common.Errors;
using PracticalTest.Domain.Write.Users;


namespace PracticalTest.Endpoint.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class AuthController:ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;
    public AuthController(IConfiguration config, IUserRepository userRepository)
    {
        _config = config;
        _userRepository = userRepository;
    }

    [HttpPost("AccessToken")]

    public async Task<IActionResult> AccessToken([FromBody] LoginCredential? credential)
    {
        var user = await _userRepository.FindUserByEmail(credential.Email,credential.Password);
        if (user is null)
        {
            throw new NotFoundUserException();
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
            new (ClaimTypes.Role, user.Role),
            new (ClaimTypes.NameIdentifier,user.UserName)
        };

        var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

