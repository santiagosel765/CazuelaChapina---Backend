using Microsoft.AspNetCore.Mvc;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Services;
using CazuelaChapina.API.Middleware;

namespace CazuelaChapina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CombosController : ControllerBase
{
    private readonly IComboService _service;

    public CombosController(IComboService service)
    {
        _service = service;
    }

    [HttpGet]
    [PermissionAuthorize("Combos", "view")]
    public async Task<ActionResult<IEnumerable<ComboDto>>> Get()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    [PermissionAuthorize("Combos", "view")]
    public async Task<ActionResult<ComboDto>> Get(int id)
    {
        var combo = await _service.GetByIdAsync(id);
        return combo is null ? NotFound() : Ok(combo);
    }

    [HttpPost]
    [PermissionAuthorize("Combos", "create")]
    public async Task<ActionResult<ComboDto>> Create(CreateComboDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [PermissionAuthorize("Combos", "update")]
    public async Task<IActionResult> Update(int id, CreateComboDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [PermissionAuthorize("Combos", "delete")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/clone")]
    [PermissionAuthorize("Combos", "create")]
    public async Task<ActionResult<ComboDto>> Clone(int id)
    {
        var created = await _service.CloneAsync(id);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPost("{id}/activate")]
    [PermissionAuthorize("Combos", "update")]
    public async Task<IActionResult> Activate(int id)
    {
        await _service.SetActiveAsync(id, true);
        return NoContent();
    }

    [HttpPost("{id}/deactivate")]
    [PermissionAuthorize("Combos", "update")]
    public async Task<IActionResult> Deactivate(int id)
    {
        await _service.SetActiveAsync(id, false);
        return NoContent();
    }
}
