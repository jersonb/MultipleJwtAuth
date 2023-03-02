using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestJwt.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = "Application")]
public class ApplicationController:ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new {result = "deu bom tb"});
    }
}