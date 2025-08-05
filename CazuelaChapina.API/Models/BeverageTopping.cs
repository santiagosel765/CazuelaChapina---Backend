namespace CazuelaChapina.API.Models;

public class BeverageTopping
{
    public int BeverageId { get; set; }
    public Beverage Beverage { get; set; } = null!;
    public int ToppingId { get; set; }
    public Topping Topping { get; set; } = null!;
}
