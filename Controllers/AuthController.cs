using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TestJwt.Models;

namespace TestJwt.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController:ControllerBase
{
    private readonly AuthUserSettings _authUserSettings;
    private readonly AuthApplicationSettings _authApplicationSettings;

    public AuthController(IOptions<AuthUserSettings> authUserSettingsOptions, IOptions<AuthApplicationSettings> authApplicationSettings)
    {
        _authUserSettings = authUserSettingsOptions.Value;
        _authApplicationSettings = authApplicationSettings.Value;
    }
    
    [HttpGet("user")]
    public IActionResult SigninUser()
    {
        var issuer = _authUserSettings.Issuer;
        var audience = _authUserSettings.Audience;
        var key = Encoding.ASCII.GetBytes(_authUserSettings.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
       // var jwtToken = tokenHandler.WriteToken(token);
        var stringToken = tokenHandler.WriteToken(token);
        
        return Ok(stringToken);
    } 
    
    [HttpGet("application")]
    public IActionResult SigninApplication()
    {
        var issuer = _authApplicationSettings.Issuer;
        var audience = _authApplicationSettings.Audience;
        var key = Encoding.ASCII.GetBytes(_authApplicationSettings.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
       // var jwtToken = tokenHandler.WriteToken(token);
        var stringToken = tokenHandler.WriteToken(token);
        
        return Ok(stringToken);
    }
}
