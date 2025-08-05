using AutoMapper;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Models;
using CazuelaChapina.API.Repositories;

namespace CazuelaChapina.API.Services;

public class ModuleService : IModuleService
{
    private readonly IRepository<Module> _moduleRepository;
    private readonly IMapper _mapper;

    public ModuleService(IRepository<Module> moduleRepository, IMapper mapper)
    {
        _moduleRepository = moduleRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ModuleDto>> GetAllAsync()
    {
        var modules = await _moduleRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ModuleDto>>(modules);
    }

    public async Task<ModuleDto?> GetByIdAsync(int id)
    {
        var module = await _moduleRepository.GetByIdAsync(id);
        return module is null ? null : _mapper.Map<ModuleDto>(module);
    }

    public async Task<ModuleDto> CreateAsync(CreateModuleDto dto)
    {
        var module = _mapper.Map<Module>(dto);
        await _moduleRepository.AddAsync(module);
        return _mapper.Map<ModuleDto>(module);
    }

    public async Task UpdateAsync(int id, UpdateModuleDto dto)
    {
        var module = await _moduleRepository.GetByIdAsync(id);
        if (module is null) return;
        _mapper.Map(dto, module);
        await _moduleRepository.UpdateAsync(module);
    }

    public async Task DeleteAsync(int id)
    {
        var module = await _moduleRepository.GetByIdAsync(id);
        if (module is null) return;
        await _moduleRepository.DeleteAsync(module);
    }
}
