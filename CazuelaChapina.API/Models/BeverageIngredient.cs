namespace CazuelaChapina.API.Models;

public class BeverageIngredient
{
    public int Id { get; set; }
    public int BeverageId { get; set; }
    public Beverage Beverage { get; set; } = null!;
    public int InventoryItemId { get; set; }
    public InventoryItem InventoryItem { get; set; } = null!;
    public decimal Quantity { get; set; }
}
