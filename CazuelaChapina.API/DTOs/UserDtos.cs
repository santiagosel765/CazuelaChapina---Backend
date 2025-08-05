namespace CazuelaChapina.API.DTOs;

public record class UserDto
{
    public UserDto() { }

    public int Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string? Phone { get; init; }
    public string Status { get; init; } = string.Empty;
    public string? Shift { get; init; }
    public int? BranchId { get; init; }
    public string? Branch { get; init; }
    public int? RoleId { get; init; }
    public string? Role { get; init; }
}

public record class CreateUserDto
{
    public CreateUserDto() { }

    public string FullName { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string? Phone { get; init; }
    public string Status { get; init; } = "Activo";
    public string? Shift { get; init; }
    public int? BranchId { get; init; }
    public int RoleId { get; init; }
}

public record class UpdateUserDto
{
    public UpdateUserDto() { }

    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string? Phone { get; init; }
    public string Status { get; init; } = "Activo";
    public string? Shift { get; init; }
    public int? BranchId { get; init; }
    public int RoleId { get; init; }
}
