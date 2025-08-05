namespace CazuelaChapina.API.Models;

public class Branch
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Manager { get; set; } = null!;

    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
