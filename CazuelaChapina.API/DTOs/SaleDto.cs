namespace CazuelaChapina.API.DTOs;

public record class SaleItemDto
{
    public SaleItemDto() { }

    public int? TamaleId { get; init; }
    public int? BeverageId { get; init; }
    public int Quantity { get; init; }
    public decimal Subtotal { get; init; }
}

public record class SaleDto
{
    public SaleDto() { }

    public int Id { get; init; }
    public DateTime Date { get; init; }
    public decimal Total { get; init; }
    public string PaymentMethod { get; init; } = string.Empty;
    public string User { get; init; } = string.Empty;
    public int BranchId { get; init; }
    public IEnumerable<SaleItemDto> Items { get; init; } = new List<SaleItemDto>();
}

public record class CreateSaleItemDto
{
    public CreateSaleItemDto() { }

    public int? TamaleId { get; init; }
    public int? BeverageId { get; init; }
    public int Quantity { get; init; }
}

public record class CreateSaleComboDto
{
    public CreateSaleComboDto() { }

    public int ComboId { get; init; }
    public int Quantity { get; init; }
}

public record class CreateSaleDto
{
    public CreateSaleDto() { }

    public int BranchId { get; init; }
    public string PaymentMethod { get; init; } = string.Empty;
    public IEnumerable<CreateSaleItemDto> Items { get; init; } = new List<CreateSaleItemDto>();
    public IEnumerable<CreateSaleComboDto> Combos { get; init; } = new List<CreateSaleComboDto>();
    public DateTime? Date { get; init; }
}
