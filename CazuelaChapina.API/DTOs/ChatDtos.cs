namespace CazuelaChapina.API.DTOs;

public record class ChatRequest
{
    public ChatRequest() { }

    public string Message { get; init; } = string.Empty;
}

public record class ChatResponse(string Response);
