using CazuelaChapina.API.DTOs;

namespace CazuelaChapina.API.Services;

public interface IBranchService
{
    Task<BranchDto> CreateAsync(CreateBranchDto dto);
    Task<IEnumerable<BranchDto>> GetAllAsync();
    Task AssignUserAsync(int branchId, int userId);
    Task<BranchReportDto?> GetReportAsync(int branchId);
}
