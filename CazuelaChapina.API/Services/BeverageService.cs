using AutoMapper;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Models;
using CazuelaChapina.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.API.Services;

public class BeverageService : IBeverageService
{
    private readonly IRepository<Beverage> _repository;
    private readonly IMapper _mapper;

    public BeverageService(IRepository<Beverage> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BeverageDto> CreateAsync(CreateBeverageDto dto)
    {
        var entity = _mapper.Map<Beverage>(dto);
        await _repository.AddAsync(entity);
        return _mapper.Map<BeverageDto>(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is not null)
            await _repository.DeleteAsync(entity);
    }

    public async Task<IEnumerable<BeverageDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync(q =>
            q.Include(b => b.Type)
             .Include(b => b.Size)
             .Include(b => b.Sweetener)
             .Include(b => b.BeverageToppings).ThenInclude(bt => bt.Topping));
        return _mapper.Map<IEnumerable<BeverageDto>>(entities);
    }

    public async Task<BeverageDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id, q =>
            q.Include(b => b.Type)
             .Include(b => b.Size)
             .Include(b => b.Sweetener)
             .Include(b => b.BeverageToppings).ThenInclude(bt => bt.Topping));
        return entity is null ? null : _mapper.Map<BeverageDto>(entity);
    }

    public async Task UpdateAsync(int id, CreateBeverageDto dto)
    {
        var entity = await _repository.GetByIdAsync(id, q => q.Include(b => b.BeverageToppings));
        if (entity is null) return;
        _mapper.Map(dto, entity);
        entity.BeverageToppings = dto.ToppingIds.Select(t => new BeverageTopping { BeverageId = id, ToppingId = t }).ToList();
        await _repository.UpdateAsync(entity);
    }
}
