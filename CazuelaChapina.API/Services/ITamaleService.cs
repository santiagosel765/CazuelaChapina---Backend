using CazuelaChapina.API.DTOs;

namespace CazuelaChapina.API.Services;

public interface ITamaleService
{
    Task<IEnumerable<TamaleDto>> GetAllAsync();
    Task<TamaleDto?> GetByIdAsync(int id);
    Task<TamaleDto> CreateAsync(CreateTamaleDto dto);
    Task UpdateAsync(int id, CreateTamaleDto dto);
    Task DeleteAsync(int id);
}
