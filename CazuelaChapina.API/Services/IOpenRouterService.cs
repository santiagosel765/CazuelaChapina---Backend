namespace CazuelaChapina.API.Services;

public interface IOpenRouterService
{
    Task<string> AskAsync(string question, string? instructions = null);
}
