using CazuelaChapina.API.DTOs;

namespace CazuelaChapina.API.Services;

public interface IBeverageService
{
    Task<IEnumerable<BeverageDto>> GetAllAsync();
    Task<BeverageDto?> GetByIdAsync(int id);
    Task<BeverageDto> CreateAsync(CreateBeverageDto dto);
    Task UpdateAsync(int id, CreateBeverageDto dto);
    Task DeleteAsync(int id);
}
