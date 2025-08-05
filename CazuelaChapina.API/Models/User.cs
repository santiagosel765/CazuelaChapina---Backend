namespace CazuelaChapina.API.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string Status { get; set; } = "Activo";
    public string? Shift { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int? BranchId { get; set; }
    public Branch? Branch { get; set; }

    public int? RoleId { get; set; }
    public Role? Role { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
