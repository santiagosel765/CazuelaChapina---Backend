namespace CazuelaChapina.API.DTOs;

public record class RoleDto
{
    public RoleDto() { }

    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public bool IsActive { get; init; }
}

public record class CreateRoleDto
{
    public CreateRoleDto() { }

    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
}

public record class UpdateRoleDto
{
    public UpdateRoleDto() { }

    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public bool IsActive { get; init; } = true;
}
