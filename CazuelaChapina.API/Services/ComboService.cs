using AutoMapper;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Models;
using CazuelaChapina.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.API.Services;

public class ComboService : IComboService
{
    private readonly IRepository<Combo> _repository;
    private readonly IMapper _mapper;

    public ComboService(IRepository<Combo> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    private static IQueryable<Combo> IncludeDetails(IQueryable<Combo> query)
    {
        return query
            .Include(c => c.Tamales).ThenInclude(ct => ct.Tamale).ThenInclude(t => t.TamaleType)
            .Include(c => c.Tamales).ThenInclude(ct => ct.Tamale).ThenInclude(t => t.Filling)
            .Include(c => c.Tamales).ThenInclude(ct => ct.Tamale).ThenInclude(t => t.Wrapper)
            .Include(c => c.Tamales).ThenInclude(ct => ct.Tamale).ThenInclude(t => t.SpiceLevel)
            .Include(c => c.Beverages).ThenInclude(cb => cb.Beverage).ThenInclude(b => b.Type)
            .Include(c => c.Beverages).ThenInclude(cb => cb.Beverage).ThenInclude(b => b.Size)
            .Include(c => c.Beverages).ThenInclude(cb => cb.Beverage).ThenInclude(b => b.Sweetener)
            .Include(c => c.Beverages).ThenInclude(cb => cb.Beverage).ThenInclude(b => b.BeverageToppings)
                .ThenInclude(bt => bt.Topping);
    }

    public async Task<ComboDto> CreateAsync(CreateComboDto dto)
    {
        var entity = _mapper.Map<Combo>(dto);
        await _repository.AddAsync(entity);
        var created = await _repository.GetByIdAsync(entity.Id, IncludeDetails);
        return _mapper.Map<ComboDto>(created!);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id, q => q.Include(c => c.Tamales).Include(c => c.Beverages));
        if (entity is not null)
            await _repository.DeleteAsync(entity);
    }

    public async Task<IEnumerable<ComboDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync(IncludeDetails);
        return _mapper.Map<IEnumerable<ComboDto>>(entities);
    }

    public async Task<ComboDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id, IncludeDetails);
        return entity is null ? null : _mapper.Map<ComboDto>(entity);
    }

    public async Task UpdateAsync(int id, CreateComboDto dto)
    {
        var entity = await _repository.GetByIdAsync(id, q => q.Include(c => c.Tamales).Include(c => c.Beverages));
        if (entity is null) return;
        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.Price = dto.Price;
        entity.IsActive = dto.IsActive;
        entity.IsEditable = dto.IsEditable;
        entity.Season = dto.Season;
        entity.Tamales = dto.Tamales.Select(t => new ComboItemTamale { ComboId = id, TamaleId = t.TamaleId, Quantity = t.Quantity }).ToList();
        entity.Beverages = dto.Beverages.Select(b => new ComboItemBeverage { ComboId = id, BeverageId = b.BeverageId, Quantity = b.Quantity }).ToList();
        await _repository.UpdateAsync(entity);
    }

    public async Task<ComboDto> CloneAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id, IncludeDetails);
        if (entity is null) throw new InvalidOperationException("Combo not found");
        var clone = new Combo
        {
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            IsActive = entity.IsActive,
            IsEditable = entity.IsEditable,
            Season = entity.Season,
            Tamales = entity.Tamales.Select(t => new ComboItemTamale { TamaleId = t.TamaleId, Quantity = t.Quantity }).ToList(),
            Beverages = entity.Beverages.Select(b => new ComboItemBeverage { BeverageId = b.BeverageId, Quantity = b.Quantity }).ToList()
        };
        await _repository.AddAsync(clone);
        var created = await _repository.GetByIdAsync(clone.Id, IncludeDetails);
        return _mapper.Map<ComboDto>(created!);
    }

    public async Task SetActiveAsync(int id, bool isActive)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null) return;
        entity.IsActive = isActive;
        await _repository.UpdateAsync(entity);
    }
    }
