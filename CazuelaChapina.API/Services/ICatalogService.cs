using AutoMapper;
using CazuelaChapina.API.Repositories;

namespace CazuelaChapina.API.Services;

public interface ICatalogService<TEntity, TDto, TCreateDto, TUpdateDto>
    where TEntity : class
{
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto?> GetByIdAsync(int id);
    Task<TDto> CreateAsync(TCreateDto dto);
    Task UpdateAsync(int id, TUpdateDto dto);
    Task DeleteAsync(int id);
}

public class CatalogService<TEntity, TDto, TCreateDto, TUpdateDto> : ICatalogService<TEntity, TDto, TCreateDto, TUpdateDto>
    where TEntity : class
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public CatalogService(IRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TDto>>(entities);
    }

    public async Task<TDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity is null ? default : _mapper.Map<TDto>(entity);
    }

    public async Task<TDto> CreateAsync(TCreateDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _repository.AddAsync(entity);
        return _mapper.Map<TDto>(entity);
    }

    public async Task UpdateAsync(int id, TUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is null) return;
        _mapper.Map(dto, entity);
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity is not null)
            await _repository.DeleteAsync(entity);
    }
}
