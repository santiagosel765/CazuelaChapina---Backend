using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CazuelaChapina.API.Services;

namespace CazuelaChapina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FaqController : ControllerBase
{
    private readonly IOpenRouterService _service;

    public FaqController(IOpenRouterService service)
    {
        _service = service;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<string>> Ask([FromQuery] string question, [FromQuery] string? instructions)
    {
        var response = await _service.AskAsync(question, instructions);
        return Ok(response);
    }
}
