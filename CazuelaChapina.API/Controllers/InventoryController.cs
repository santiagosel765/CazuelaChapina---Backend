using Microsoft.AspNetCore.Mvc;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Services;
using CazuelaChapina.API.Middleware;

namespace CazuelaChapina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _service;

    public InventoryController(IInventoryService service)
    {
        _service = service;
    }

    [HttpGet]
    [PermissionAuthorize("Inventory", "view")]
    public async Task<ActionResult<IEnumerable<InventoryItemDto>>> Get()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    [PermissionAuthorize("Inventory", "view")]
    public async Task<ActionResult<InventoryItemDto>> Get(int id)
    {
        var item = await _service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    [PermissionAuthorize("Inventory", "create")]
    public async Task<ActionResult<InventoryItemDto>> Create(CreateInventoryItemDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [PermissionAuthorize("Inventory", "update")]
    public async Task<IActionResult> Update(int id, CreateInventoryItemDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [PermissionAuthorize("Inventory", "delete")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("entries/recent")]
    [PermissionAuthorize("Inventory", "view")]
    public async Task<ActionResult<IEnumerable<InventoryEntryDto>>> GetRecentEntries()
        => Ok(await _service.GetRecentEntriesAsync());

    [HttpPost("{id}/entry")]
    [PermissionAuthorize("Inventory", "update")]
    public async Task<ActionResult<InventoryEntryDto>> RegisterEntry(int id, CreateInventoryEntryDto dto)
    {
        var user = User.Identity?.Name ?? "unknown";
        try
        {
            var entry = await _service.RegisterEntryAsync(id, dto, user);
            return Ok(entry);
        }
        catch (InvalidOperationException ex)
        {
            if (ex.Message == "Item not found.")
                return NotFound(new { message = ex.Message });
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("{id}/exit")]
    [PermissionAuthorize("Inventory", "update")]
    public async Task<ActionResult> RegisterExit(int id, CreateInventoryEntryDto dto)
    {
        var user = User.Identity?.Name ?? "unknown";
        try
        {
            var entry = await _service.RegisterExitAsync(id, dto, user);
            return Ok(entry);
        }
        catch (InvalidOperationException ex)
        {
            if (ex.Message == "Item not found.")
                return NotFound(new { message = ex.Message });
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("{id}/waste")]
    [PermissionAuthorize("Inventory", "update")]
    public async Task<ActionResult> RegisterWaste(int id, CreateInventoryEntryDto dto)
    {
        var user = User.Identity?.Name ?? "unknown";
        try
        {
            var entry = await _service.RegisterWasteAsync(id, dto, user);
            return Ok(entry);
        }
        catch (InvalidOperationException ex)
        {
            if (ex.Message == "Item not found.")
                return NotFound(new { message = ex.Message });
            return BadRequest(new { message = ex.Message });
        }
    }
}
