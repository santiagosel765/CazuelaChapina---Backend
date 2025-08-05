using CazuelaChapina.API.DTOs;

namespace CazuelaChapina.API.Services;

public interface IModuleService
{
    Task<IEnumerable<ModuleDto>> GetAllAsync();
    Task<ModuleDto?> GetByIdAsync(int id);
    Task<ModuleDto> CreateAsync(CreateModuleDto dto);
    Task UpdateAsync(int id, UpdateModuleDto dto);
    Task DeleteAsync(int id);
}
