namespace CazuelaChapina.API.Models;

public class BeverageType
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Beverage> Beverages { get; set; } = new List<Beverage>();
}
