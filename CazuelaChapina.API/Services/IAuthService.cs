using CazuelaChapina.API.Models;
using CazuelaChapina.API.DTOs;

namespace CazuelaChapina.API.Services;

public interface IAuthService
{
    Task<string?> LoginAsync(string username, string password);
    Task<User> RegisterAsync(string username, string password, string role);
    Task AssignRoleAsync(int userId, int roleId);
    Task SetPermissionAsync(int roleId, PermissionDto permission);
}
