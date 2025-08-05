using System.Text.Json;

namespace CazuelaChapina.API.Services;

public class OpenRouterService : IOpenRouterService
{
    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    private const string BasePrompt =
        "Eres el asistente virtual de Cazuela Chapina, una tienda de tamales, bebidas típicas, combos y sus ingredientes. Responde en español de forma clara y breve. Si la pregunta no se relaciona con estos productos o no sabes la respuesta, responde 'Consulta con un vendedor'.";
    private const string FallbackResponse = "Consulta con un vendedor";

    public OpenRouterService(HttpClient client, IConfiguration config)
    {
        _client = client;
        _config = config;
        _client.BaseAddress = new Uri("https://openrouter.ai/api");
        var key = _config["OpenRouter:ApiKey"];
        if (!string.IsNullOrWhiteSpace(key))
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {key}");
    }

    public async Task<string> AskAsync(string question, string? instructions = null)
    {
        var systemPrompt = string.IsNullOrWhiteSpace(instructions)
            ? BasePrompt
            : $"{BasePrompt} {instructions}";

        var payload = new
        {
            model = _config["OpenRouter:Model"] ?? "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "system", content = systemPrompt },
                new { role = "user", content = question }
            }
        };

        try
        {
            var response = await _client.PostAsJsonAsync("/v1/chat/completions", payload);
            if (!response.IsSuccessStatusCode)
                return FallbackResponse;
            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            var message = json.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
            return string.IsNullOrWhiteSpace(message) ? FallbackResponse : message.Trim();
        }
        catch
        {
            return FallbackResponse;
        }
    }
}
