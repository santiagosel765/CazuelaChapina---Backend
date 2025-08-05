using Microsoft.AspNetCore.Mvc;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Models;
using CazuelaChapina.API.Services;
using CazuelaChapina.API.Middleware;

namespace CazuelaChapina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SweetenersController : ControllerBase
{
    private readonly ICatalogService<Sweetener, SweetenerDto, CreateSweetenerDto, UpdateSweetenerDto> _service;

    public SweetenersController(ICatalogService<Sweetener, SweetenerDto, CreateSweetenerDto, UpdateSweetenerDto> service)
    {
        _service = service;
    }

    [HttpGet]
    [PermissionAuthorize("Catalogs", "view")]
    public async Task<ActionResult<IEnumerable<SweetenerDto>>> Get()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    [PermissionAuthorize("Catalogs", "view")]
    public async Task<ActionResult<SweetenerDto>> Get(int id)
    {
        var item = await _service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    [PermissionAuthorize("Catalogs", "create")]
    public async Task<ActionResult<SweetenerDto>> Create(CreateSweetenerDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [PermissionAuthorize("Catalogs", "update")]
    public async Task<IActionResult> Update(int id, UpdateSweetenerDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [PermissionAuthorize("Catalogs", "delete")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
