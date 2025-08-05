using CazuelaChapina.API.Models;
using CazuelaChapina.API.Repositories;
using System.Linq;

namespace CazuelaChapina.API.Services;

public class DeviceService : IDeviceService
{
    private readonly IRepository<Device> _deviceRepository;
    private readonly IRepository<User> _userRepository;

    public DeviceService(IRepository<Device> deviceRepository, IRepository<User> userRepository)
    {
        _deviceRepository = deviceRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<string>> GetAllTokensAsync()
    {
        var devices = await _deviceRepository.GetAllAsync();
        return devices.Select(d => d.Token);
    }

    public async Task RegisterAsync(string token, string username)
    {
        var user = (await _userRepository.GetAllAsync(q => q.Where(u => u.Username == username))).FirstOrDefault();
        if (user is null)
            throw new InvalidOperationException("User not found.");

        var existing = (await _deviceRepository.GetAllAsync(q => q.Where(d => d.Token == token))).FirstOrDefault();
        if (existing is null)
        {
            await _deviceRepository.AddAsync(new Device { Token = token, UserId = user.Id });
        }
        else if (existing.UserId != user.Id)
        {
            existing.UserId = user.Id;
            await _deviceRepository.UpdateAsync(existing);
        }
    }
}
