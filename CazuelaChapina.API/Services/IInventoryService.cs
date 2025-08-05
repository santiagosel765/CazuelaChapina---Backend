using CazuelaChapina.API.DTOs;

namespace CazuelaChapina.API.Services;

public interface IInventoryService
{
    Task<IEnumerable<InventoryItemDto>> GetAllAsync();
    Task<InventoryItemDto?> GetByIdAsync(int id);
    Task<IEnumerable<InventoryEntryDto>> GetRecentEntriesAsync(int count = 20);
    Task<InventoryItemDto> CreateAsync(CreateInventoryItemDto dto);
    Task UpdateAsync(int id, CreateInventoryItemDto dto);
    Task DeleteAsync(int id);

    Task<InventoryEntryDto> RegisterEntryAsync(int itemId, CreateInventoryEntryDto dto, string user);
    Task<InventoryEntryDto> RegisterExitAsync(int itemId, CreateInventoryEntryDto dto, string user);
    Task<InventoryEntryDto> RegisterWasteAsync(int itemId, CreateInventoryEntryDto dto, string user);
}
