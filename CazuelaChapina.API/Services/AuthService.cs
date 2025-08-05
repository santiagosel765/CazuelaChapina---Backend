using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using AutoMapper;
using CazuelaChapina.API.Data;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CazuelaChapina.API.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public AuthService(AppDbContext context, IConfiguration configuration, IMapper mapper)
    {
        _context = context;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<string?> LoginAsync(string username, string password)
    {
        var user = await _context.Users
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                    .ThenInclude(r => r.Permissions)
                        .ThenInclude(p => p.Module)
            .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);

        if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Username) };
        foreach (var role in user.UserRoles.Select(r => r.Role.Name))
            claims.Add(new Claim(ClaimTypes.Role, role));

        var permissions = user.UserRoles
            .SelectMany(ur => ur.Role.Permissions)
            .Select(p => new
            {
                module = p.Module.Name,
                actions = new List<string>()
                {
                    p.CanView ? "view" : null,
                    p.CanCreate ? "create" : null,
                    p.CanUpdate ? "update" : null,
                    p.CanDelete ? "delete" : null
                }.Where(a => a != null)
            });

        claims.Add(new Claim("permissions", JsonSerializer.Serialize(permissions)));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<User> RegisterAsync(string username, string password, string roleName)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        if (role is null)
        {
            role = new Role { Name = roleName };
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        var user = new User
        {
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
        };
        user.UserRoles.Add(new UserRole { RoleId = role.Id, Role = role });

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task AssignRoleAsync(int userId, int roleId)
    {
        var user = await _context.Users.Include(u => u.UserRoles).FirstOrDefaultAsync(u => u.Id == userId);
        var role = await _context.Roles.FindAsync(roleId);
        if (user is null || role is null) return;

        user.UserRoles.Clear();
        user.UserRoles.Add(new UserRole { UserId = userId, RoleId = roleId });
        user.RoleId = roleId;

        await _context.SaveChangesAsync();
    }

    public async Task SetPermissionAsync(int roleId, PermissionDto dto)
    {
        var permission = await _context.Permissions
            .FirstOrDefaultAsync(p => p.RoleId == roleId && p.ModuleId == dto.ModuleId);

        if (permission is null)
        {
            permission = _mapper.Map<Permission>(dto);
            permission.RoleId = roleId;
            _context.Permissions.Add(permission);
        }
        else
        {
            permission.CanView = dto.CanView;
            permission.CanCreate = dto.CanCreate;
            permission.CanUpdate = dto.CanUpdate;
            permission.CanDelete = dto.CanDelete;
        }

        await _context.SaveChangesAsync();
    }
}
