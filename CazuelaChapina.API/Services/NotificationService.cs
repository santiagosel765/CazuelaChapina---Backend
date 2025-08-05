using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace CazuelaChapina.API.Services;

public class NotificationService : INotificationService
{
    private readonly IDeviceService _deviceService;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public NotificationService(IDeviceService deviceService, HttpClient httpClient, IConfiguration config)
    {
        _deviceService = deviceService;
        _httpClient = httpClient;
        _config = config;
    }

    public async Task SendAsync(string title, string body, IDictionary<string, string>? data = null)
    {
        var tokens = (await _deviceService.GetAllTokensAsync()).ToArray();
        if (tokens.Length == 0) return;

        var payload = new
        {
            registration_ids = tokens,
            notification = new { title, body },
            data
        };

        var json = JsonSerializer.Serialize(payload);
        using var content = new StringContent(json, Encoding.UTF8, "application/json");

        var serverKey = _config["Fcm:ServerKey"];
        if (string.IsNullOrWhiteSpace(serverKey))
            return;
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={serverKey}");
        await _httpClient.PostAsync("https://fcm.googleapis.com/fcm/send", content);
    }
}
