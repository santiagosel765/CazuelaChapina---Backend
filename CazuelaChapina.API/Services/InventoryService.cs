using AutoMapper;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Models;
using CazuelaChapina.API.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CazuelaChapina.API.Services;

public class InventoryService : IInventoryService
{
    private readonly IRepository<InventoryItem> _repository;
    private readonly IRepository<InventoryEntry> _entryRepository;
    private readonly IMapper _mapper;

    public InventoryService(IRepository<InventoryItem> repository, IRepository<InventoryEntry> entryRepository, IMapper mapper)
    {
        _repository = repository;
        _entryRepository = entryRepository;
        _mapper = mapper;
    }

    public async Task<InventoryItemDto> CreateAsync(CreateInventoryItemDto dto)
    {
        var entity = _mapper.Map<InventoryItem>(dto);
        await _repository.AddAsync(entity);
        return _mapper.Map<InventoryItemDto>(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is not null)
            await _repository.DeleteAsync(entity);
    }

    public async Task<IEnumerable<InventoryItemDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<InventoryItemDto>>(entities);
    }

    public async Task<InventoryItemDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? null : _mapper.Map<InventoryItemDto>(entity);
    }

    public async Task<IEnumerable<InventoryEntryDto>> GetRecentEntriesAsync(int count = 20)
    {
        var entries = await _entryRepository.GetAllAsync(q =>
            q.Include(e => e.Item)
             .OrderByDescending(e => e.Date)
             .Take(count));
        return _mapper.Map<IEnumerable<InventoryEntryDto>>(entries);
    }

    public async Task UpdateAsync(int id, CreateInventoryItemDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null) return;
        _mapper.Map(dto, entity);
        await _repository.UpdateAsync(entity);
    }

    private async Task<InventoryEntryDto> RegisterAsync(int itemId, CreateInventoryEntryDto dto, InventoryOperationType type, string user)
    {
        var item = await _repository.GetByIdAsync(itemId);
        if (item is null)
            throw new InvalidOperationException("Item not found.");

        if (type != InventoryOperationType.Entry)
        {
            if (item.IsCritical && item.Stock <= 0)
                throw new InvalidOperationException("Critical item out of stock.");
            if (item.Stock < dto.Quantity)
                throw new InvalidOperationException($"Insufficient stock for {item.Name}.");
            item.Stock -= dto.Quantity;
        }
        else
        {
            item.Stock += dto.Quantity;
        }

        await _repository.UpdateAsync(item);

        var entry = _mapper.Map<InventoryEntry>(dto);
        entry.InventoryItemId = itemId;
        entry.OperationType = type;
        entry.RegisteredBy = user;
        entry.Date = DateTime.UtcNow;

        await _entryRepository.AddAsync(entry);
        return _mapper.Map<InventoryEntryDto>(entry);
    }

    public Task<InventoryEntryDto> RegisterEntryAsync(int itemId, CreateInventoryEntryDto dto, string user)
        => RegisterAsync(itemId, dto, InventoryOperationType.Entry, user);

    public Task<InventoryEntryDto> RegisterExitAsync(int itemId, CreateInventoryEntryDto dto, string user)
        => RegisterAsync(itemId, dto, InventoryOperationType.Exit, user);

    public Task<InventoryEntryDto> RegisterWasteAsync(int itemId, CreateInventoryEntryDto dto, string user)
        => RegisterAsync(itemId, dto, InventoryOperationType.Waste, user);
}
