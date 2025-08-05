using AutoMapper;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Models;
using CazuelaChapina.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.API.Services;

public class TamaleService : ITamaleService
{
    private readonly IRepository<Tamale> _repository;
    private readonly IMapper _mapper;

    public TamaleService(IRepository<Tamale> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TamaleDto> CreateAsync(CreateTamaleDto dto)
    {
        var entity = _mapper.Map<Tamale>(dto);
        await _repository.AddAsync(entity);
        return _mapper.Map<TamaleDto>(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is not null)
            await _repository.DeleteAsync(entity);
    }

    public async Task<IEnumerable<TamaleDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync(q =>
            q.Include(t => t.TamaleType)
             .Include(t => t.Filling)
             .Include(t => t.Wrapper)
             .Include(t => t.SpiceLevel));
        return _mapper.Map<IEnumerable<TamaleDto>>(entities);
    }

    public async Task<TamaleDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id, q =>
            q.Include(t => t.TamaleType)
             .Include(t => t.Filling)
             .Include(t => t.Wrapper)
             .Include(t => t.SpiceLevel));
        return entity is null ? null : _mapper.Map<TamaleDto>(entity);
    }

    public async Task UpdateAsync(int id, CreateTamaleDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null) return;
        _mapper.Map(dto, entity);
        await _repository.UpdateAsync(entity);
    }
}
