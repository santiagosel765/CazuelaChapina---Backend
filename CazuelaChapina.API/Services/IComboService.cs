using CazuelaChapina.API.DTOs;

namespace CazuelaChapina.API.Services;

public interface IComboService
{
    Task<IEnumerable<ComboDto>> GetAllAsync();
    Task<ComboDto?> GetByIdAsync(int id);
    Task<ComboDto> CreateAsync(CreateComboDto dto);
    Task UpdateAsync(int id, CreateComboDto dto);
    Task DeleteAsync(int id);
    Task<ComboDto> CloneAsync(int id);
    Task SetActiveAsync(int id, bool isActive);
}
