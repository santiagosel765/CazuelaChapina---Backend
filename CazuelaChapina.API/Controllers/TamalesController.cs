using Microsoft.AspNetCore.Mvc;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Services;
using CazuelaChapina.API.Middleware;

namespace CazuelaChapina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TamalesController : ControllerBase
{
    private readonly ITamaleService _service;

    public TamalesController(ITamaleService service)
    {
        _service = service;
    }

    [HttpGet]
    [PermissionAuthorize("Tamales", "view")]
    public async Task<ActionResult<IEnumerable<TamaleDto>>> Get()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    [PermissionAuthorize("Tamales", "view")]
    public async Task<ActionResult<TamaleDto>> Get(int id)
    {
        var tamal = await _service.GetByIdAsync(id);
        return tamal is null ? NotFound() : Ok(tamal);
    }

    [HttpPost]
    [PermissionAuthorize("Tamales", "create")]
    public async Task<ActionResult<TamaleDto>> Create(CreateTamaleDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [PermissionAuthorize("Tamales", "update")]
    public async Task<IActionResult> Update(int id, CreateTamaleDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [PermissionAuthorize("Tamales", "delete")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
