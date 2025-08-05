namespace CazuelaChapina.API.Services;

public interface INotificationService
{
    Task SendAsync(string title, string body, IDictionary<string, string>? data = null);
}
