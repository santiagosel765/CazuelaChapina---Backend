using System.Net.Http;
using System.Text;
using System.Text.Json;
using CazuelaChapina.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CazuelaChapina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;

    public ChatController(IHttpClientFactory httpClientFactory, IConfiguration config)
    {
        _httpClientFactory = httpClientFactory;
        _config = config;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<ChatResponse>> Post([FromBody] ChatRequest request)
    {
        var apiKey = _config["OpenRouter:ApiKey"] ?? Environment.GetEnvironmentVariable("OPENROUTER_API_KEY");
        if (string.IsNullOrWhiteSpace(apiKey))
            return StatusCode(500, new { error = "API key not configured." });

        var payload = new
        {
            model = "openai/gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "system", content = "Eres un asistente para clientes de La Cazuela Chapina, una empresa que vende tamales y bebidas típicas guatemaltecas." },
                new { role = "user", content = request.Message }
            }
        };

        try
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://openrouter.ai/api/v1/chat/completions");
            httpRequest.Headers.Add("Authorization", $"Bearer {apiKey}");
            httpRequest.Headers.Add("HTTP-Referer", "http://localhost:3000");
            httpRequest.Headers.Add("X-Title", "CazuelaChapina");
            httpRequest.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            var response = await client.SendAsync(httpRequest);
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, new { error = "No se pudo obtener una respuesta en este momento." });

            using var stream = await response.Content.ReadAsStreamAsync();
            var json = await JsonDocument.ParseAsync(stream);
            var message = json.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

            return Ok(new ChatResponse(message ?? string.Empty));
        }
        catch
        {
            return StatusCode(500, new { error = "No se pudo obtener una respuesta en este momento." });
        }
    }
}
