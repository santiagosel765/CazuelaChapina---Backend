using Microsoft.AspNetCore.Mvc;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Models;
using CazuelaChapina.API.Services;
using CazuelaChapina.API.Middleware;

namespace CazuelaChapina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToppingsController : ControllerBase
{
    private readonly ICatalogService<Topping, ToppingDto, CreateToppingDto, UpdateToppingDto> _service;

    public ToppingsController(ICatalogService<Topping, ToppingDto, CreateToppingDto, UpdateToppingDto> service)
    {
        _service = service;
    }

    [HttpGet]
    [PermissionAuthorize("Catalogs", "view")]
    public async Task<ActionResult<IEnumerable<ToppingDto>>> Get()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    [PermissionAuthorize("Catalogs", "view")]
    public async Task<ActionResult<ToppingDto>> Get(int id)
    {
        var item = await _service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    [PermissionAuthorize("Catalogs", "create")]
    public async Task<ActionResult<ToppingDto>> Create(CreateToppingDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [PermissionAuthorize("Catalogs", "update")]
    public async Task<IActionResult> Update(int id, UpdateToppingDto dto)
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
