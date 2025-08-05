using Microsoft.AspNetCore.Mvc;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Services;
using CazuelaChapina.API.Middleware;

namespace CazuelaChapina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BeveragesController : ControllerBase
{
    private readonly IBeverageService _service;

    public BeveragesController(IBeverageService service)
    {
        _service = service;
    }

    [HttpGet]
    [PermissionAuthorize("Beverages", "view")]
    public async Task<ActionResult<IEnumerable<BeverageDto>>> Get()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    [PermissionAuthorize("Beverages", "view")]
    public async Task<ActionResult<BeverageDto>> Get(int id)
    {
        var beverage = await _service.GetByIdAsync(id);
        return beverage is null ? NotFound() : Ok(beverage);
    }

    [HttpPost]
    [PermissionAuthorize("Beverages", "create")]
    public async Task<ActionResult<BeverageDto>> Create(CreateBeverageDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [PermissionAuthorize("Beverages", "update")]
    public async Task<IActionResult> Update(int id, CreateBeverageDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [PermissionAuthorize("Beverages", "delete")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
