using Microsoft.AspNetCore.Mvc;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Services;
using CazuelaChapina.API.Middleware;

namespace CazuelaChapina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BranchesController : ControllerBase
{
    private readonly IBranchService _service;

    public BranchesController(IBranchService service)
    {
        _service = service;
    }

    [HttpGet]
    [PermissionAuthorize("Sales", "view")]
    public async Task<ActionResult<IEnumerable<BranchDto>>> Get()
        => Ok(await _service.GetAllAsync());

    [HttpPost]
    [PermissionAuthorize("Sales", "create")]
    public async Task<ActionResult<BranchDto>> Create(CreateBranchDto dto)
    {
        var branch = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(Get), new { id = branch.Id }, branch);
    }

    [HttpPost("{branchId}/assign/{userId}")]
    [PermissionAuthorize("Sales", "update")]
    public async Task<IActionResult> Assign(int branchId, int userId)
    {
        try
        {
            await _service.AssignUserAsync(branchId, userId);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("{id}/report")]
    [PermissionAuthorize("Sales", "view")]
    public async Task<ActionResult<BranchReportDto>> Report(int id)
    {
        var report = await _service.GetReportAsync(id);
        return report is null ? NotFound() : Ok(report);
    }
}
