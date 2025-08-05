using Microsoft.AspNetCore.Mvc;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Services;
using CazuelaChapina.API.Middleware;

namespace CazuelaChapina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModulesController : ControllerBase
{
    private readonly IModuleService _moduleService;

    public ModulesController(IModuleService moduleService)
    {
        _moduleService = moduleService;
    }

    [HttpGet]
    [PermissionAuthorize("Users", "view")]
    public async Task<IEnumerable<ModuleDto>> GetAll()
        => await _moduleService.GetAllAsync();

    [HttpGet("{id}")]
    [PermissionAuthorize("Users", "view")]
    public async Task<ActionResult<ModuleDto>> GetById(int id)
    {
        var module = await _moduleService.GetByIdAsync(id);
        if (module is null) return NotFound();
        return Ok(module);
    }

    [HttpPost]
    [PermissionAuthorize("Users", "create")]
    public async Task<ActionResult<ModuleDto>> Create(CreateModuleDto dto)
    {
        var module = await _moduleService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = module.Id }, module);
    }

    [HttpPut("{id}")]
    [PermissionAuthorize("Users", "update")]
    public async Task<IActionResult> Update(int id, UpdateModuleDto dto)
    {
        await _moduleService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [PermissionAuthorize("Users", "delete")]
    public async Task<IActionResult> Delete(int id)
    {
        await _moduleService.DeleteAsync(id);
        return NoContent();
    }
}
