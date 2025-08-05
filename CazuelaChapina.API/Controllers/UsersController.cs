using Microsoft.AspNetCore.Mvc;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Services;
using CazuelaChapina.API.Middleware;

namespace CazuelaChapina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public UsersController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpGet]
    [PermissionAuthorize("Users", "view")]
    public async Task<IEnumerable<UserDto>> GetAll()
        => await _userService.GetAllAsync();

    [HttpGet("{id}")]
    [PermissionAuthorize("Users", "view")]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user is null) return NotFound();
        return Ok(user);
    }

    [HttpPost]
    [PermissionAuthorize("Users", "create")]
    public async Task<ActionResult<UserDto>> Create(CreateUserDto dto)
    {
        var user = await _userService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    [PermissionAuthorize("Users", "update")]
    public async Task<IActionResult> Update(int id, UpdateUserDto dto)
    {
        await _userService.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpPut("{id}/status")]
    [PermissionAuthorize("Users", "update")]
    public async Task<IActionResult> ChangeStatus(int id, [FromBody] string status)
    {
        await _userService.ChangeStatusAsync(id, status);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [PermissionAuthorize("Users", "delete")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/roles")]
    [PermissionAuthorize("Users", "update")]
    public async Task<IActionResult> AssignRole(int id, AssignRoleRequest request)
    {
        await _authService.AssignRoleAsync(id, request.RoleId);
        return NoContent();
    }
}
