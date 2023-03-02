using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestJwt.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = "User")]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new {resut ="deu bom"});
    }
    
}
