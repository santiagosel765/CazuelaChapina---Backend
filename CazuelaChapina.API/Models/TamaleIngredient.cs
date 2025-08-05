namespace CazuelaChapina.API.Models;

public class TamaleIngredient
{
    public int Id { get; set; }
    public int TamaleId { get; set; }
    public Tamale Tamale { get; set; } = null!;
    public int InventoryItemId { get; set; }
    public InventoryItem InventoryItem { get; set; } = null!;
    public decimal Quantity { get; set; }
}
