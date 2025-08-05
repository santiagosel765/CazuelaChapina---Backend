using CazuelaChapina.API.Models;

namespace CazuelaChapina.API.DTOs;

public record class InventoryItemDto
{
    public InventoryItemDto() { }

    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public InventoryItemType Type { get; init; }
    public decimal Stock { get; init; }
    public decimal UnitCost { get; init; }
    public bool IsCritical { get; init; }
}

public record class CreateInventoryItemDto
{
    public CreateInventoryItemDto() { }

    public string Name { get; init; } = string.Empty;
    public InventoryItemType Type { get; init; }
    public decimal Stock { get; init; }
    public decimal UnitCost { get; init; }
    public bool IsCritical { get; init; }
}
