using Microsoft.AspNetCore.Mvc;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Services;
using CazuelaChapina.API.Middleware;

namespace CazuelaChapina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SyncController : ControllerBase
{
    private readonly ISaleService _saleService;

    public SyncController(ISaleService saleService)
    {
        _saleService = saleService;
    }

    [HttpPost("sale")]
    [PermissionAuthorize("Sales", "create")]
    public async Task<ActionResult<SaleDto>> SyncSale(CreateSaleDto dto)
    {
        var user = User.Identity?.Name ?? "unknown";
        try
        {
            var sale = await _saleService.CreateAsync(dto, user);
            return CreatedAtAction(nameof(SalesController.Get), "Sales", new { id = sale.Id }, sale);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
