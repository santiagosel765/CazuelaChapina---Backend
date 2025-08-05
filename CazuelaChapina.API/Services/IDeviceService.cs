namespace CazuelaChapina.API.Services;

public interface IDeviceService
{
    Task RegisterAsync(string token, string username);
    Task<IEnumerable<string>> GetAllTokensAsync();
}
