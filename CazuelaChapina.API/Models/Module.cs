namespace CazuelaChapina.API.Models;

public class Module
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
