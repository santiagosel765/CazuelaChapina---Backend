using Microsoft.AspNetCore.Mvc;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Services;
using CazuelaChapina.API.Middleware;
using System.Collections.Generic;

namespace CazuelaChapina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISaleService _service;
    private readonly INotificationService _notificationService;

    public SalesController(ISaleService service, INotificationService notificationService)
    {
        _service = service;
        _notificationService = notificationService;
    }

    [HttpGet]
    [PermissionAuthorize("Sales", "view")]
    public async Task<ActionResult<IEnumerable<SaleDto>>> Get()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    [PermissionAuthorize("Sales", "view")]
    public async Task<ActionResult<SaleDto>> Get(int id)
    {
        var sale = await _service.GetByIdAsync(id);
        return sale is null ? NotFound() : Ok(sale);
    }

    [HttpPost]
    [PermissionAuthorize("Sales", "create")]
    public async Task<ActionResult<SaleDto>> Create(CreateSaleDto dto)
    {
        var user = User.Identity?.Name ?? "unknown";
        try
        {
            var sale = await _service.CreateAsync(dto, user);
            await _notificationService.SendAsync(
                "Nueva venta",
                $"Venta #{sale.Id} registrada",
                new Dictionary<string, string>
                {
                    { "saleId", sale.Id.ToString() },
                    { "total", sale.Total.ToString() }
                });
            return CreatedAtAction(nameof(Get), new { id = sale.Id }, sale);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
