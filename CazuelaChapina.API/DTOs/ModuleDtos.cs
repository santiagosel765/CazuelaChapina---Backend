namespace CazuelaChapina.API.DTOs;

public record class ModuleDto
{
    public ModuleDto() { }

    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
}

public record class CreateModuleDto
{
    public CreateModuleDto() { }

    public string Name { get; init; } = string.Empty;
}

public record class UpdateModuleDto
{
    public UpdateModuleDto() { }

    public string Name { get; init; } = string.Empty;
}
