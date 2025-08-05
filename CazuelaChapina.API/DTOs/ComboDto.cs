using CazuelaChapina.API.Models;

namespace CazuelaChapina.API.DTOs;

public record class ComboItemTamaleDto
{
    public ComboItemTamaleDto() { }

    public int TamaleId { get; init; }
    public int Quantity { get; init; }
    public TamaleDto? Tamale { get; init; }
}

public record class ComboItemBeverageDto
{
    public ComboItemBeverageDto() { }

    public int BeverageId { get; init; }
    public int Quantity { get; init; }
    public BeverageDto? Beverage { get; init; }
}

public record class ComboDto
{
    public ComboDto() { }

    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public bool IsActive { get; init; }
    public bool IsEditable { get; init; }
    public string Season { get; init; } = string.Empty;
    public IEnumerable<ComboItemTamaleDto> Tamales { get; init; } = new List<ComboItemTamaleDto>();
    public IEnumerable<ComboItemBeverageDto> Beverages { get; init; } = new List<ComboItemBeverageDto>();
    public decimal Total { get; init; }
}

public record class CreateComboDto
{
    public CreateComboDto() { }

    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public bool IsActive { get; init; }
    public bool IsEditable { get; init; }
    public Season Season { get; init; }
    public IEnumerable<ComboItemTamaleDto> Tamales { get; init; } = new List<ComboItemTamaleDto>();
    public IEnumerable<ComboItemBeverageDto> Beverages { get; init; } = new List<ComboItemBeverageDto>();
}
