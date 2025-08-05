namespace CazuelaChapina.API.DTOs;

public record class TamaleTypeDto
{
    public TamaleTypeDto() { }
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}

public record class CreateTamaleTypeDto
{
    public CreateTamaleTypeDto() { }
    public string Name { get; init; } = string.Empty;
}

public record class UpdateTamaleTypeDto
{
    public UpdateTamaleTypeDto() { }
    public string Name { get; init; } = string.Empty;
}

public record class FillingDto
{
    public FillingDto() { }
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}

public record class CreateFillingDto
{
    public CreateFillingDto() { }
    public string Name { get; init; } = string.Empty;
}

public record class UpdateFillingDto
{
    public UpdateFillingDto() { }
    public string Name { get; init; } = string.Empty;
}

public record class WrapperDto
{
    public WrapperDto() { }
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}

public record class CreateWrapperDto
{
    public CreateWrapperDto() { }
    public string Name { get; init; } = string.Empty;
}

public record class UpdateWrapperDto
{
    public UpdateWrapperDto() { }
    public string Name { get; init; } = string.Empty;
}

public record class SpiceLevelDto
{
    public SpiceLevelDto() { }
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}

public record class CreateSpiceLevelDto
{
    public CreateSpiceLevelDto() { }
    public string Name { get; init; } = string.Empty;
}

public record class UpdateSpiceLevelDto
{
    public UpdateSpiceLevelDto() { }
    public string Name { get; init; } = string.Empty;
}

public record class BeverageTypeDto
{
    public BeverageTypeDto() { }
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}

public record class CreateBeverageTypeDto
{
    public CreateBeverageTypeDto() { }
    public string Name { get; init; } = string.Empty;
}

public record class UpdateBeverageTypeDto
{
    public UpdateBeverageTypeDto() { }
    public string Name { get; init; } = string.Empty;
}

public record class BeverageSizeDto
{
    public BeverageSizeDto() { }
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}

public record class CreateBeverageSizeDto
{
    public CreateBeverageSizeDto() { }
    public string Name { get; init; } = string.Empty;
}

public record class UpdateBeverageSizeDto
{
    public UpdateBeverageSizeDto() { }
    public string Name { get; init; } = string.Empty;
}

public record class SweetenerDto
{
    public SweetenerDto() { }
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}

public record class CreateSweetenerDto
{
    public CreateSweetenerDto() { }
    public string Name { get; init; } = string.Empty;
}

public record class UpdateSweetenerDto
{
    public UpdateSweetenerDto() { }
    public string Name { get; init; } = string.Empty;
}

public record class ToppingDto
{
    public ToppingDto() { }
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}

public record class CreateToppingDto
{
    public CreateToppingDto() { }
    public string Name { get; init; } = string.Empty;
}

public record class UpdateToppingDto
{
    public UpdateToppingDto() { }
    public string Name { get; init; } = string.Empty;
}
