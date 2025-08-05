using AutoMapper;
using CazuelaChapina.API.Data;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Mappings;
using CazuelaChapina.API.Models;
using CazuelaChapina.API.Repositories;
using CazuelaChapina.API.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Collections.Generic;

public class SaleServiceTests
{
    private static (SaleService svc, AppDbContext ctx) CreateService()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var ctx = new AppDbContext(options);
        var mapper = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())).CreateMapper();
        var saleRepo = new Repository<Sale>(ctx);
        var invRepo = new Repository<InventoryItem>(ctx);
        var entryRepo = new Repository<InventoryEntry>(ctx);
        var tamaleRepo = new Repository<Tamale>(ctx);
        var beverageRepo = new Repository<Beverage>(ctx);
        var tamaleIngRepo = new Repository<TamaleIngredient>(ctx);
        var bevIngRepo = new Repository<BeverageIngredient>(ctx);
        var userRepo = new Repository<User>(ctx);
        var branchRepo = new Repository<Branch>(ctx);
        var comboRepo = new Repository<Combo>(ctx);
        var svc = new SaleService(saleRepo, invRepo, entryRepo, tamaleRepo, beverageRepo, tamaleIngRepo, bevIngRepo, userRepo, branchRepo, comboRepo, mapper, ctx);
        return (svc, ctx);
    }

    [Fact]
    public async Task CreateAsync_Offline_UsesProvidedDate()
    {
        var (svc, ctx) = CreateService();
        var branch = await ctx.Branches.AddAsync(new Branch { Name = "Central", Address = "Main", Manager = "Boss" });
        var user = await ctx.Users.AddAsync(new User { Username = "user", PasswordHash = "pass", BranchId = branch.Entity.Id });
        var item = await ctx.InventoryItems.AddAsync(new InventoryItem { Name = "Corn", Type = InventoryItemType.RawMaterial, Stock = 10, UnitCost = 1, IsCritical = false });
        var tamale = await ctx.Tamales.AddAsync(new Tamale {
            TamaleType = new TamaleType { Name = "Type" },
            Filling = new Filling { Name = "Fill" },
            Wrapper = new Wrapper { Name = "Wrap" },
            SpiceLevel = new SpiceLevel { Name = "Mild" },
            Price = 5
        });
        await ctx.SaveChangesAsync();
        await ctx.TamaleIngredients.AddAsync(new TamaleIngredient { TamaleId = tamale.Entity.Id, InventoryItemId = item.Entity.Id, Quantity = 1 });
        await ctx.SaveChangesAsync();

        var dto = new CreateSaleDto
        {
            BranchId = branch.Entity.Id,
            PaymentMethod = "cash",
            Items = new[] { new CreateSaleItemDto { TamaleId = tamale.Entity.Id, Quantity = 1 } },
            Date = new DateTime(2020,1,1)
        };
        var sale = await svc.CreateAsync(dto, user.Entity.Username);
        Assert.Equal(new DateTime(2020,1,1), sale.Date);
        Assert.Equal(branch.Entity.Id, sale.BranchId);
    }

    [Fact]
    public async Task CreateAsync_WithCombo_ProcessesComboItems()
    {
        var (svc, ctx) = CreateService();
        var branch = await ctx.Branches.AddAsync(new Branch { Name = "Central", Address = "Main", Manager = "Boss" });
        var user = await ctx.Users.AddAsync(new User { Username = "user", PasswordHash = "pass", BranchId = branch.Entity.Id });

        var itemT = await ctx.InventoryItems.AddAsync(new InventoryItem { Name = "Corn", Type = InventoryItemType.RawMaterial, Stock = 10, UnitCost = 1, IsCritical = false });
        var itemB = await ctx.InventoryItems.AddAsync(new InventoryItem { Name = "Sugar", Type = InventoryItemType.RawMaterial, Stock = 10, UnitCost = 1, IsCritical = false });

        var tamale = await ctx.Tamales.AddAsync(new Tamale
        {
            TamaleType = new TamaleType { Name = "Type" },
            Filling = new Filling { Name = "Fill" },
            Wrapper = new Wrapper { Name = "Wrap" },
            SpiceLevel = new SpiceLevel { Name = "Mild" },
            Price = 5
        });
        var beverage = await ctx.Beverages.AddAsync(new Beverage
        {
            Type = new BeverageType { Name = "T" },
            Size = new BeverageSize { Name = "S" },
            Sweetener = new Sweetener { Name = "Swe" },
            Price = 3
        });
        await ctx.SaveChangesAsync();

        await ctx.TamaleIngredients.AddAsync(new TamaleIngredient { TamaleId = tamale.Entity.Id, InventoryItemId = itemT.Entity.Id, Quantity = 1 });
        await ctx.BeverageIngredients.AddAsync(new BeverageIngredient { BeverageId = beverage.Entity.Id, InventoryItemId = itemB.Entity.Id, Quantity = 1 });
        await ctx.SaveChangesAsync();

        var combo = await ctx.Combos.AddAsync(new Combo
        {
            Name = "Combo",
            Description = string.Empty,
            Price = 7,
            IsActive = true,
            IsEditable = true,
            Season = Season.None,
            Tamales = new List<ComboItemTamale> { new ComboItemTamale { TamaleId = tamale.Entity.Id, Quantity = 1 } },
            Beverages = new List<ComboItemBeverage> { new ComboItemBeverage { BeverageId = beverage.Entity.Id, Quantity = 1 } }
        });
        await ctx.SaveChangesAsync();

        var dto = new CreateSaleDto
        {
            BranchId = branch.Entity.Id,
            PaymentMethod = "cash",
            Combos = new[] { new CreateSaleComboDto { ComboId = combo.Entity.Id, Quantity = 2 } }
        };

        var sale = await svc.CreateAsync(dto, user.Entity.Username);

        Assert.Equal(14m, sale.Total);
        Assert.Equal(8, (await ctx.InventoryItems.FindAsync(itemT.Entity.Id))!.Stock);
        Assert.Equal(8, (await ctx.InventoryItems.FindAsync(itemB.Entity.Id))!.Stock);
    }
}
