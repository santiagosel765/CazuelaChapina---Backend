using Microsoft.AspNetCore.Mvc;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Services;
using CazuelaChapina.API.Middleware;

namespace CazuelaChapina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    [PermissionAuthorize("Users", "view")]
    public async Task<IEnumerable<RoleDto>> GetAll()
        => await _roleService.GetAllAsync();

    [HttpGet("{id}")]
    [PermissionAuthorize("Users", "view")]
    public async Task<ActionResult<RoleDto>> GetById(int id)
    {
        var role = await _roleService.GetByIdAsync(id);
        if (role is null) return NotFound();
        return Ok(role);
    }

    [HttpPost]
    [PermissionAuthorize("Users", "create")]
    public async Task<ActionResult<RoleDto>> Create(CreateRoleDto dto)
    {
        var role = await _roleService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = role.Id }, role);
    }

    [HttpPut("{id}")]
    [PermissionAuthorize("Users", "update")]
    public async Task<IActionResult> Update(int id, UpdateRoleDto dto)
    {
        await _roleService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [PermissionAuthorize("Users", "delete")]
    public async Task<IActionResult> Delete(int id)
    {
        await _roleService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id}/permissions")]
    [PermissionAuthorize("Users", "view")]
    public async Task<IEnumerable<PermissionDto>> GetPermissions(int id)
        => await _roleService.GetPermissionsAsync(id);

    [HttpPost("{id}/permissions")]
    [PermissionAuthorize("Users", "update")]
    public async Task<IActionResult> SetPermissions(int id, RolePermissionsDto dto)
    {
        await _roleService.SetPermissionsAsync(id, dto.Permissions);
        return NoContent();
    }
}
