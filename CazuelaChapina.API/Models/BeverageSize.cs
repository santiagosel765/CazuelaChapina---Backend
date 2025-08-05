namespace CazuelaChapina.API.Models;

public class BeverageSize
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Beverage> Beverages { get; set; } = new List<Beverage>();
}
