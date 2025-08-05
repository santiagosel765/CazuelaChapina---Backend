using CazuelaChapina.API.DTOs;

namespace CazuelaChapina.API.Services;

public interface IRoleService
{
    Task<IEnumerable<RoleDto>> GetAllAsync();
    Task<RoleDto?> GetByIdAsync(int id);
    Task<RoleDto> CreateAsync(CreateRoleDto dto);
    Task UpdateAsync(int id, UpdateRoleDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<PermissionDto>> GetPermissionsAsync(int roleId);
    Task SetPermissionsAsync(int roleId, IEnumerable<PermissionDto> permissions);
}
