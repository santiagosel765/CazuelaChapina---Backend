namespace CazuelaChapina.API.Models;

public class ComboItemBeverage
{
    public int ComboId { get; set; }
    public Combo Combo { get; set; } = null!;
    public int BeverageId { get; set; }
    public Beverage Beverage { get; set; } = null!;
    public int Quantity { get; set; }
}
