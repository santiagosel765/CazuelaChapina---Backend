using System.Collections.Generic;

namespace CazuelaChapina.API.DTOs;

public record class AssignRoleRequest
{
    public AssignRoleRequest() { }

    public int RoleId { get; init; }
}

public record class PermissionDto
{
    public PermissionDto() { }

    public int ModuleId { get; init; }
    public bool CanView { get; init; }
    public bool CanCreate { get; init; }
    public bool CanUpdate { get; init; }
    public bool CanDelete { get; init; }
}

public record class RolePermissionsDto
{
    public RolePermissionsDto() { }

    public int RoleId { get; init; }
    public IEnumerable<PermissionDto> Permissions { get; init; } = new List<PermissionDto>();
}
