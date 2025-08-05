namespace CazuelaChapina.API.Models;

public class Filling
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Tamale> Tamales { get; set; } = new List<Tamale>();
}
