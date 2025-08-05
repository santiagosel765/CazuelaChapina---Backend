using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CazuelaChapina.API.Data;
using CazuelaChapina.API.Middleware;
using CazuelaChapina.API.Models;

namespace CazuelaChapina.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("summary")]
    [PermissionAuthorize("Dashboard", "view")]
    public async Task<ActionResult<object>> GetSummary([FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null, [FromQuery] int? branchId = null)
    {
        var saleItemsQuery = _context.SaleItems
            .Include(i => i.Sale)
            .Include(i => i.Tamale).ThenInclude(t => t.SpiceLevel)
            .Include(i => i.Tamale).ThenInclude(t => t.Filling)
            .Include(i => i.Tamale).ThenInclude(t => t.TamaleType)
            .Include(i => i.Beverage).ThenInclude(b => b.Type)
            .Include(i => i.Beverage).ThenInclude(b => b.Size)
            .AsQueryable();

        if (startDate.HasValue)
        {
            saleItemsQuery = saleItemsQuery.Where(i => i.Sale.Date >= ToUtcDate(startDate.Value));
        }
        if (endDate.HasValue)
        {
            saleItemsQuery = saleItemsQuery.Where(i => i.Sale.Date < ToUtcDate(endDate.Value.AddDays(1)));
        }
        if (branchId.HasValue)
        {
            saleItemsQuery = saleItemsQuery.Where(i => i.Sale.BranchId == branchId.Value);
        }

        var saleItems = await saleItemsQuery.ToListAsync();

        var today = DateTime.UtcNow.Date;
        var monthStart = DateTime.SpecifyKind(new DateTime(today.Year, today.Month, 1), DateTimeKind.Utc);
        var monthEnd = monthStart.AddMonths(1);

        var dailySales = saleItems
            .Where(i => i.Sale.Date >= today && i.Sale.Date < today.AddDays(1))
            .Sum(i => i.Subtotal);
        var monthlySales = saleItems
            .Where(i => i.Sale.Date >= monthStart && i.Sale.Date < monthEnd)
            .Sum(i => i.Subtotal);

        var topTamales = saleItems
            .Where(i => i.TamaleId != null)
            .GroupBy(i => i.Tamale!)
            .Select(g => new
            {
                id = g.Key.Id,
                name = $"{g.Key.TamaleType.Name} de {g.Key.Filling.Name}",
                quantity = g.Sum(i => i.Quantity)
            })
            .OrderByDescending(g => g.quantity)
            .Take(3)
            .ToList();

        var beverageItems = saleItems
            .Where(i => i.BeverageId != null)
            .Select(i => new { Item = i, Hour = i.Sale.Date.Hour });

        var morning = beverageItems
            .Where(x => x.Hour >= 6 && x.Hour < 12)
            .GroupBy(x => x.Item.Beverage!)
            .Select(g => new { name = $"{g.Key.Type.Name} {g.Key.Size.Name}", quantity = g.Sum(x => x.Item.Quantity) })
            .OrderByDescending(g => g.quantity)
            .FirstOrDefault();

        var afternoon = beverageItems
            .Where(x => x.Hour >= 12 && x.Hour < 18)
            .GroupBy(x => x.Item.Beverage!)
            .Select(g => new { name = $"{g.Key.Type.Name} {g.Key.Size.Name}", quantity = g.Sum(x => x.Item.Quantity) })
            .OrderByDescending(g => g.quantity)
            .FirstOrDefault();

        var night = beverageItems
            .Where(x => x.Hour >= 18 && x.Hour < 24)
            .GroupBy(x => x.Item.Beverage!)
            .Select(g => new { name = $"{g.Key.Type.Name} {g.Key.Size.Name}", quantity = g.Sum(x => x.Item.Quantity) })
            .OrderByDescending(g => g.quantity)
            .FirstOrDefault();

        var tamaleItems = saleItems.Where(i => i.TamaleId != null);
        var spicyCount = tamaleItems
            .Where(i => i.Tamale!.SpiceLevel.Name == "Picante")
            .Sum(i => i.Quantity);
        var nonSpicyCount = tamaleItems
            .Where(i => i.Tamale!.SpiceLevel.Name != "Picante")
            .Sum(i => i.Quantity);

        var tamaleCosts = await _context.TamaleIngredients
            .Include(ti => ti.InventoryItem)
            .GroupBy(ti => ti.TamaleId)
            .Select(g => new { TamaleId = g.Key, Cost = g.Sum(ti => ti.Quantity * ti.InventoryItem.UnitCost) })
            .ToDictionaryAsync(x => x.TamaleId, x => x.Cost);

        var beverageCosts = await _context.BeverageIngredients
            .Include(bi => bi.InventoryItem)
            .GroupBy(bi => bi.BeverageId)
            .Select(g => new { BeverageId = g.Key, Cost = g.Sum(bi => bi.Quantity * bi.InventoryItem.UnitCost) })
            .ToDictionaryAsync(x => x.BeverageId, x => x.Cost);

        var tamaleRevenue = tamaleItems.Sum(i => i.Subtotal);
        var tamaleCost = tamaleItems.Sum(i => i.Quantity * (tamaleCosts.TryGetValue(i.TamaleId!.Value, out var c) ? c : 0m));
        var tamaleProfit = tamaleRevenue - tamaleCost;

        var beverageOnly = saleItems.Where(i => i.BeverageId != null);
        var beverageRevenue = beverageOnly.Sum(i => i.Subtotal);
        var beverageCost = beverageOnly.Sum(i => i.Quantity * (beverageCosts.TryGetValue(i.BeverageId!.Value, out var c) ? c : 0m));
        var beverageProfit = beverageRevenue - beverageCost;

        var comboProfit = 0m;

        var wasteQuery = _context.InventoryEntries
            .Include(e => e.Item)
            .Where(e => e.OperationType == InventoryOperationType.Waste)
            .Where(e => e.Date >= monthStart && e.Date < monthEnd);

        if (startDate.HasValue)
        {
            wasteQuery = wasteQuery.Where(e => e.Date >= ToUtcDate(startDate.Value));
        }
        if (endDate.HasValue)
        {
            wasteQuery = wasteQuery.Where(e => e.Date < ToUtcDate(endDate.Value.AddDays(1)));
        }

        var wasteQuantity = await wasteQuery.SumAsync(e => e.Quantity);
        var wasteCost = await wasteQuery.SumAsync(e => e.Quantity * e.Item!.UnitCost);

        return Ok(new
        {
            sales = new { day = dailySales, month = monthlySales },
            topTamales,
            popularBeverages = new { morning, afternoon, night },
            spiceLevel = new { spicy = spicyCount, nonSpicy = nonSpicyCount },
            profit = new { tamales = tamaleProfit, beverages = beverageProfit, combos = comboProfit },
            waste = new { quantity = wasteQuantity, cost = wasteCost }
        });
    }

    private static DateTime ToUtcDate(DateTime date) =>
        DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);
}

