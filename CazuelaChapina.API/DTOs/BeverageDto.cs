namespace CazuelaChapina.API.DTOs;

public record class BeverageDto
{
    public BeverageDto() { }

    public int Id { get; init; }
    public string Type { get; init; } = string.Empty;
    public string Size { get; init; } = string.Empty;
    public string Sweetener { get; init; } = string.Empty;
    public IEnumerable<string> Toppings { get; init; } = new List<string>();
    public decimal Price { get; init; }
}

public record class CreateBeverageDto
{
    public CreateBeverageDto() { }

    public int TypeId { get; init; }
    public int SizeId { get; init; }
    public int SweetenerId { get; init; }
    public IEnumerable<int> ToppingIds { get; init; } = new List<int>();
    public decimal Price { get; init; }
}
