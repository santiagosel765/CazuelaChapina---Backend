using CazuelaChapina.API.DTOs;

namespace CazuelaChapina.API.Services;

public interface ISaleService
{
    Task<SaleDto> CreateAsync(CreateSaleDto dto, string username);
    Task<IEnumerable<SaleDto>> GetAllAsync();
    Task<SaleDto?> GetByIdAsync(int id);
}
