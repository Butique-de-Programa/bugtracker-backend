using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello, worlddd!");
    }
}