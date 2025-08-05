using CazuelaChapina.API.Models;

namespace CazuelaChapina.API.DTOs;

public record class InventoryEntryDto
{
    public InventoryEntryDto() { }

    public int Id { get; init; }
    public int InventoryItemId { get; init; }
    public string ItemName { get; init; } = string.Empty;
    public InventoryOperationType OperationType { get; init; }
    public decimal Quantity { get; init; }
    public string RegisteredBy { get; init; } = string.Empty;
    public string? Reason { get; init; }
    public DateTime Date { get; init; }
}

public record class CreateInventoryEntryDto
{
    public CreateInventoryEntryDto() { }

    public decimal Quantity { get; init; }
    public string? Reason { get; init; }
}
