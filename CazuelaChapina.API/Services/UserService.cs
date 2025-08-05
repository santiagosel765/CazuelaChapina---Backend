using AutoMapper;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Models;
using CazuelaChapina.API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CazuelaChapina.API.Services;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public UserService(IRepository<User> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync(q => q
            .Include(u => u.Branch)
            .Include(u => u.Role));
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id, q => q
            .Include(u => u.Branch)
            .Include(u => u.Role));
        return user is null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto)
    {
        var user = _mapper.Map<User>(dto);
        user.UserRoles.Add(new UserRole { RoleId = dto.RoleId });
        await _userRepository.AddAsync(user);
        return _mapper.Map<UserDto>(user);
    }

    public async Task UpdateAsync(int id, UpdateUserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null) return;
        _mapper.Map(dto, user);
        await _userRepository.UpdateAsync(user);
    }

    public async Task ChangeStatusAsync(int id, string status)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null) return;
        user.Status = status;
        user.IsActive = status.Equals("Activo", StringComparison.OrdinalIgnoreCase);
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null) return;
        user.Status = "Eliminado";
        user.IsActive = false;
        await _userRepository.UpdateAsync(user);
    }
}
