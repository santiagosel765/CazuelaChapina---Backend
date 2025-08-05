namespace CazuelaChapina.API.Models;

public class InventoryItem
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public InventoryItemType Type { get; set; }
    public decimal Stock { get; set; }
    public decimal UnitCost { get; set; }
    public bool IsCritical { get; set; }
}
