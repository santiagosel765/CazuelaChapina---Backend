using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Services;
using System.Collections.Generic;

namespace CazuelaChapina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly IDeviceService _deviceService;
    private readonly INotificationService _notificationService;

    public NotificationsController(IDeviceService deviceService, INotificationService notificationService)
    {
        _deviceService = deviceService;
        _notificationService = notificationService;
    }

    [HttpPost("register")]
    [Authorize]
    public async Task<IActionResult> Register(RegisterDeviceDto dto)
    {
        var user = User.Identity?.Name ?? "unknown";
        await _deviceService.RegisterAsync(dto.Token, user);
        return Ok();
    }

    [HttpPost("cooking-complete")]
    [Authorize]
    public async Task<IActionResult> CookingComplete()
    {
        await _notificationService.SendAsync(
            "Cocción finalizada",
            "Un lote de tamales está listo",
            new Dictionary<string, string> { { "event", "cooking_complete" } });
        return Ok();
    }
}
