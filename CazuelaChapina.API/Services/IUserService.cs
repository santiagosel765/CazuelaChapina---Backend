using CazuelaChapina.API.DTOs;

namespace CazuelaChapina.API.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(int id);
    Task<UserDto> CreateAsync(CreateUserDto dto);
    Task UpdateAsync(int id, UpdateUserDto dto);
    Task ChangeStatusAsync(int id, string status);
    Task DeleteAsync(int id);
}
