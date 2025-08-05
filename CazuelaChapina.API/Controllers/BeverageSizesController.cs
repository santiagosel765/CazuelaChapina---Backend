using Microsoft.AspNetCore.Mvc;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Models;
using CazuelaChapina.API.Services;
using CazuelaChapina.API.Middleware;

namespace CazuelaChapina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BeverageSizesController : ControllerBase
{
    private readonly ICatalogService<BeverageSize, BeverageSizeDto, CreateBeverageSizeDto, UpdateBeverageSizeDto> _service;

    public BeverageSizesController(ICatalogService<BeverageSize, BeverageSizeDto, CreateBeverageSizeDto, UpdateBeverageSizeDto> service)
    {
        _service = service;
    }

    [HttpGet]
    [PermissionAuthorize("Catalogs", "view")]
    public async Task<ActionResult<IEnumerable<BeverageSizeDto>>> Get()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    [PermissionAuthorize("Catalogs", "view")]
    public async Task<ActionResult<BeverageSizeDto>> Get(int id)
    {
        var item = await _service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    [PermissionAuthorize("Catalogs", "create")]
    public async Task<ActionResult<BeverageSizeDto>> Create(CreateBeverageSizeDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [PermissionAuthorize("Catalogs", "update")]
    public async Task<IActionResult> Update(int id, UpdateBeverageSizeDto dto)
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
