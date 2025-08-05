using AutoMapper;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Models;
using CazuelaChapina.API.Repositories;
using System.Linq;

namespace CazuelaChapina.API.Services;

public class RoleService : IRoleService
{
    private readonly IRepository<Role> _roleRepository;
    private readonly IRepository<Permission> _permissionRepository;
    private readonly IMapper _mapper;

    public RoleService(IRepository<Role> roleRepository, IRepository<Permission> permissionRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoleDto>> GetAllAsync()
    {
        var roles = await _roleRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<RoleDto>>(roles);
    }

    public async Task<RoleDto?> GetByIdAsync(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        return role is null ? null : _mapper.Map<RoleDto>(role);
    }

    public async Task<RoleDto> CreateAsync(CreateRoleDto dto)
    {
        var role = _mapper.Map<Role>(dto);
        await _roleRepository.AddAsync(role);
        return _mapper.Map<RoleDto>(role);
    }

    public async Task UpdateAsync(int id, UpdateRoleDto dto)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role is null) return;
        _mapper.Map(dto, role);
        await _roleRepository.UpdateAsync(role);
    }

    public async Task DeleteAsync(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role is null) return;
        await _roleRepository.DeleteAsync(role);
    }

    public async Task<IEnumerable<PermissionDto>> GetPermissionsAsync(int roleId)
    {
        var permissions = await _permissionRepository.GetAllAsync(q => q.Where(p => p.RoleId == roleId));
        return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }

    public async Task SetPermissionsAsync(int roleId, IEnumerable<PermissionDto> permissions)
    {
        var existing = (await _permissionRepository.GetAllAsync(q => q.Where(p => p.RoleId == roleId))).ToList();
        foreach (var dto in permissions)
        {
            var perm = existing.FirstOrDefault(p => p.ModuleId == dto.ModuleId);
            if (perm is null)
            {
                perm = _mapper.Map<Permission>(dto);
                perm.RoleId = roleId;
                await _permissionRepository.AddAsync(perm);
            }
            else
            {
                perm.CanView = dto.CanView;
                perm.CanCreate = dto.CanCreate;
                perm.CanUpdate = dto.CanUpdate;
                perm.CanDelete = dto.CanDelete;
                await _permissionRepository.UpdateAsync(perm);
            }
        }
    }
}
