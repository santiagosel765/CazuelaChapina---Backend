namespace CazuelaChapina.API.DTOs;

public record class BranchDto
{
    public BranchDto() { }

    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public string Manager { get; init; } = string.Empty;
}

public record class CreateBranchDto
{
    public CreateBranchDto() { }

    public string Name { get; init; } = string.Empty;
    public string Address { get; init; } = string.Empty;
    public string Manager { get; init; } = string.Empty;
}

public record class BranchReportDto
{
    public BranchReportDto() { }

    public BranchReportDto(int id, string name, int count, decimal total)
    {
    }

    public int BranchId { get; init; }
    public string BranchName { get; init; } = string.Empty;
    public int SalesCount { get; init; }
    public decimal TotalAmount { get; init; }
}
