namespace CazuelaChapina.API.Models;

public class InventoryEntry
{
    public int Id { get; set; }
    public int InventoryItemId { get; set; }
    public InventoryItem? Item { get; set; }
    public InventoryOperationType OperationType { get; set; }
    public decimal Quantity { get; set; }
    public string RegisteredBy { get; set; } = null!;
    public string? Reason { get; set; }
    public DateTime Date { get; set; }
}
