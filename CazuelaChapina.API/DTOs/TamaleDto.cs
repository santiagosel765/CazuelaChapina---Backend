namespace CazuelaChapina.API.DTOs;

public record class TamaleDto
{
    public TamaleDto() { }

    public int Id { get; init; }
    public string TamaleType { get; init; } = string.Empty;
    public string Filling { get; init; } = string.Empty;
    public string Wrapper { get; init; } = string.Empty;
    public string SpiceLevel { get; init; } = string.Empty;
    public decimal Price { get; init; }
}

public record class CreateTamaleDto
{
    public CreateTamaleDto() { }

    public int TamaleTypeId { get; init; }
    public int FillingId { get; init; }
    public int WrapperId { get; init; }
    public int SpiceLevelId { get; init; }
    public decimal Price { get; init; }
}
