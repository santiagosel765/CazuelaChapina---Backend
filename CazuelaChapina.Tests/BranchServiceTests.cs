using AutoMapper;
using CazuelaChapina.API.Data;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Mappings;
using CazuelaChapina.API.Models;
using CazuelaChapina.API.Repositories;
using CazuelaChapina.API.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class BranchServiceTests
{
    private static (BranchService svc, AppDbContext ctx) CreateService()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var ctx = new AppDbContext(options);
        var mapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())).CreateMapper();
        var repo = new Repository<Branch>(ctx);
        var userRepo = new Repository<User>(ctx);
        var saleRepo = new Repository<Sale>(ctx);
        var svc = new BranchService(repo, userRepo, saleRepo, mapper);
        return (svc, ctx);
    }

    [Fact]
    public async Task AssignUserAsync_SetsBranchId()
    {
        var (svc, ctx) = CreateService();
        var branch = await ctx.Branches.AddAsync(new Branch { Name = "Central", Address = "Main", Manager = "Boss" });
        var user = await ctx.Users.AddAsync(new User { Username = "user", PasswordHash = "pass" });
        await ctx.SaveChangesAsync();

        await svc.AssignUserAsync(branch.Entity.Id, user.Entity.Id);
        var updated = await ctx.Users.FindAsync(user.Entity.Id);
        Assert.Equal(branch.Entity.Id, updated!.BranchId);
    }
}
