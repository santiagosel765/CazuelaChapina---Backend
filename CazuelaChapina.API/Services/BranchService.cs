using AutoMapper;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Models;
using CazuelaChapina.API.Repositories;
using System.Linq;

namespace CazuelaChapina.API.Services;

public class BranchService : IBranchService
{
    private readonly IRepository<Branch> _branchRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Sale> _saleRepository;
    private readonly IMapper _mapper;

    public BranchService(IRepository<Branch> branchRepository, IRepository<User> userRepository, IRepository<Sale> saleRepository, IMapper mapper)
    {
        _branchRepository = branchRepository;
        _userRepository = userRepository;
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<BranchDto> CreateAsync(CreateBranchDto dto)
    {
        var branch = _mapper.Map<Branch>(dto);
        await _branchRepository.AddAsync(branch);
        return _mapper.Map<BranchDto>(branch);
    }

    public async Task<IEnumerable<BranchDto>> GetAllAsync()
    {
        var branches = await _branchRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BranchDto>>(branches);
    }

    public async Task AssignUserAsync(int branchId, int userId)
    {
        var branch = await _branchRepository.GetByIdAsync(branchId);
        if (branch is null) throw new InvalidOperationException("Branch not found.");
        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null) throw new InvalidOperationException("User not found.");
        user.BranchId = branchId;
        await _userRepository.UpdateAsync(user);
    }

    public async Task<BranchReportDto?> GetReportAsync(int branchId)
    {
        var branch = await _branchRepository.GetByIdAsync(branchId);
        if (branch is null) return null;
        var sales = await _saleRepository.GetAllAsync(q => q.Where(s => s.BranchId == branchId));
        var count = sales.Count();
        var total = sales.Sum(s => s.Total);
        return new BranchReportDto(branch.Id, branch.Name, count, total);
    }
}
