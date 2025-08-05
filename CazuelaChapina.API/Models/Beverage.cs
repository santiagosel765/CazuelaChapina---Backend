namespace CazuelaChapina.API.Models;

public class Beverage
{
    public int Id { get; set; }
    public int TypeId { get; set; }
    public BeverageType Type { get; set; } = null!;
    public int SizeId { get; set; }
    public BeverageSize Size { get; set; } = null!;
    public int SweetenerId { get; set; }
    public Sweetener Sweetener { get; set; } = null!;
    public decimal Price { get; set; }
    public ICollection<BeverageTopping> BeverageToppings { get; set; } = new List<BeverageTopping>();
    public ICollection<ComboItemBeverage> ComboItems { get; set; } = new List<ComboItemBeverage>();
}
