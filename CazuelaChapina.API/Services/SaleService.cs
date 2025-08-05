using AutoMapper;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Models;
using CazuelaChapina.API.Repositories;
using CazuelaChapina.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CazuelaChapina.API.Services;

public class SaleService : ISaleService
{
    private readonly IRepository<Sale> _saleRepository;
    private readonly IRepository<InventoryItem> _inventoryRepository;
    private readonly IRepository<InventoryEntry> _entryRepository;
    private readonly IRepository<Tamale> _tamaleRepository;
    private readonly IRepository<Beverage> _beverageRepository;
    private readonly IRepository<TamaleIngredient> _tamaleIngredientRepository;
    private readonly IRepository<BeverageIngredient> _beverageIngredientRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Branch> _branchRepository;
    private readonly IRepository<Combo> _comboRepository;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public SaleService(
        IRepository<Sale> saleRepository,
        IRepository<InventoryItem> inventoryRepository,
        IRepository<InventoryEntry> entryRepository,
        IRepository<Tamale> tamaleRepository,
        IRepository<Beverage> beverageRepository,
        IRepository<TamaleIngredient> tamaleIngredientRepository,
        IRepository<BeverageIngredient> beverageIngredientRepository,
        IRepository<User> userRepository,
        IRepository<Branch> branchRepository,
        IRepository<Combo> comboRepository,
        IMapper mapper,
        AppDbContext context)
    {
        _saleRepository = saleRepository;
        _inventoryRepository = inventoryRepository;
        _entryRepository = entryRepository;
        _tamaleRepository = tamaleRepository;
        _beverageRepository = beverageRepository;
        _tamaleIngredientRepository = tamaleIngredientRepository;
        _beverageIngredientRepository = beverageIngredientRepository;
        _userRepository = userRepository;
        _branchRepository = branchRepository;
        _comboRepository = comboRepository;
        _mapper = mapper;
        _context = context;
    }

    public async Task<SaleDto> CreateAsync(CreateSaleDto dto, string username)
    {
        var user = (await _userRepository.GetAllAsync(q => q.Where(u => u.Username == username))).FirstOrDefault();
        if (user is null)
            throw new InvalidOperationException("User not found.");
        var branch = await _branchRepository.GetByIdAsync(dto.BranchId);
        if (branch is null)
            throw new InvalidOperationException("Branch not found.");

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var sale = new Sale
            {
                Date = dto.Date ?? DateTime.UtcNow,
                PaymentMethod = dto.PaymentMethod,
                UserId = user.Id,
                BranchId = dto.BranchId,
                Items = new List<SaleItem>()
            };

            decimal comboTotal = 0m;

            foreach (var itemDto in dto.Items)
            {
                if (itemDto.TamaleId.HasValue)
                {
                    var tamale = await _tamaleRepository.GetByIdAsync(itemDto.TamaleId.Value);
                    if (tamale is null) throw new InvalidOperationException("Tamale not found.");

                    var saleItem = new SaleItem
                    {
                        TamaleId = tamale.Id,
                        Quantity = itemDto.Quantity,
                        Subtotal = tamale.Price * itemDto.Quantity
                    };
                    sale.Items.Add(saleItem);

                    var ingredients = await _tamaleIngredientRepository.GetAllAsync(q => q.Where(r => r.TamaleId == tamale.Id));
                    foreach (var ing in ingredients)
                    {
                        var required = ing.Quantity * itemDto.Quantity;
                        var inventoryItem = await _inventoryRepository.GetByIdAsync(ing.InventoryItemId);
                        if (inventoryItem is null)
                            throw new InvalidOperationException("Ingredient not found.");
                        if (inventoryItem.Stock < required)
                            throw new InvalidOperationException($"Insufficient stock for {inventoryItem.Name}.");
                        inventoryItem.Stock -= required;
                        await _inventoryRepository.UpdateAsync(inventoryItem);

                        var entry = new InventoryEntry
                        {
                            InventoryItemId = inventoryItem.Id,
                            OperationType = InventoryOperationType.Exit,
                            Quantity = required,
                            RegisteredBy = username,
                            Date = DateTime.UtcNow,
                            Reason = "Sale"
                        };
                        await _entryRepository.AddAsync(entry);
                    }
                }
                else if (itemDto.BeverageId.HasValue)
                {
                    var beverage = await _beverageRepository.GetByIdAsync(itemDto.BeverageId.Value);
                    if (beverage is null) throw new InvalidOperationException("Beverage not found.");

                    var saleItem = new SaleItem
                    {
                        BeverageId = beverage.Id,
                        Quantity = itemDto.Quantity,
                        Subtotal = beverage.Price * itemDto.Quantity
                    };
                    sale.Items.Add(saleItem);

                    var ingredients = await _beverageIngredientRepository.GetAllAsync(q => q.Where(r => r.BeverageId == beverage.Id));
                    foreach (var ing in ingredients)
                    {
                        var required = ing.Quantity * itemDto.Quantity;
                        var inventoryItem = await _inventoryRepository.GetByIdAsync(ing.InventoryItemId);
                        if (inventoryItem is null)
                            throw new InvalidOperationException("Ingredient not found.");
                        if (inventoryItem.Stock < required)
                            throw new InvalidOperationException($"Insufficient stock for {inventoryItem.Name}.");
                        inventoryItem.Stock -= required;
                        await _inventoryRepository.UpdateAsync(inventoryItem);

                        var entry = new InventoryEntry
                        {
                            InventoryItemId = inventoryItem.Id,
                            OperationType = InventoryOperationType.Exit,
                            Quantity = required,
                            RegisteredBy = username,
                            Date = DateTime.UtcNow,
                            Reason = "Sale"
                        };
                        await _entryRepository.AddAsync(entry);
                    }
                }
            }

            foreach (var comboDto in dto.Combos)
            {
                var combo = await _comboRepository.GetByIdAsync(comboDto.ComboId, q => q
                    .Include(c => c.Tamales)
                    .Include(c => c.Beverages));
                if (combo is null) throw new InvalidOperationException("Combo not found.");

                comboTotal += combo.Price * comboDto.Quantity;

                foreach (var ct in combo.Tamales)
                {
                    var totalQty = ct.Quantity * comboDto.Quantity;
                    var saleItem = new SaleItem
                    {
                        TamaleId = ct.TamaleId,
                        Quantity = totalQty,
                        Subtotal = 0m
                    };
                    sale.Items.Add(saleItem);

                    var ingredients = await _tamaleIngredientRepository.GetAllAsync(q => q.Where(r => r.TamaleId == ct.TamaleId));
                    foreach (var ing in ingredients)
                    {
                        var required = ing.Quantity * totalQty;
                        var inventoryItem = await _inventoryRepository.GetByIdAsync(ing.InventoryItemId);
                        if (inventoryItem is null)
                            throw new InvalidOperationException("Ingredient not found.");
                        if (inventoryItem.Stock < required)
                            throw new InvalidOperationException($"Insufficient stock for {inventoryItem.Name}.");
                        inventoryItem.Stock -= required;
                        await _inventoryRepository.UpdateAsync(inventoryItem);

                        var entry = new InventoryEntry
                        {
                            InventoryItemId = inventoryItem.Id,
                            OperationType = InventoryOperationType.Exit,
                            Quantity = required,
                            RegisteredBy = username,
                            Date = DateTime.UtcNow,
                            Reason = "Sale",
                        };
                        await _entryRepository.AddAsync(entry);
                    }
                }

                foreach (var cb in combo.Beverages)
                {
                    var totalQty = cb.Quantity * comboDto.Quantity;
                    var saleItem = new SaleItem
                    {
                        BeverageId = cb.BeverageId,
                        Quantity = totalQty,
                        Subtotal = 0m
                    };
                    sale.Items.Add(saleItem);

                    var ingredients = await _beverageIngredientRepository.GetAllAsync(q => q.Where(r => r.BeverageId == cb.BeverageId));
                    foreach (var ing in ingredients)
                    {
                        var required = ing.Quantity * totalQty;
                        var inventoryItem = await _inventoryRepository.GetByIdAsync(ing.InventoryItemId);
                        if (inventoryItem is null)
                            throw new InvalidOperationException("Ingredient not found.");
                        if (inventoryItem.Stock < required)
                            throw new InvalidOperationException($"Insufficient stock for {inventoryItem.Name}.");
                        inventoryItem.Stock -= required;
                        await _inventoryRepository.UpdateAsync(inventoryItem);

                        var entry = new InventoryEntry
                        {
                            InventoryItemId = inventoryItem.Id,
                            OperationType = InventoryOperationType.Exit,
                            Quantity = required,
                            RegisteredBy = username,
                            Date = DateTime.UtcNow,
                            Reason = "Sale",
                        };
                        await _entryRepository.AddAsync(entry);
                    }
                }
            }

            sale.Total = sale.Items.Sum(i => i.Subtotal) + comboTotal;

            await _saleRepository.AddAsync(sale);
            await transaction.CommitAsync();

            var created = await _saleRepository.GetByIdAsync(sale.Id, q => q
                .Include(s => s.User)
                .Include(s => s.Branch)
                .Include(s => s.Items).ThenInclude(i => i.Tamale)
                .Include(s => s.Items).ThenInclude(i => i.Beverage));

            return _mapper.Map<SaleDto>(created!);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<IEnumerable<SaleDto>> GetAllAsync()
    {
        var sales = await _saleRepository.GetAllAsync(q => q
            .Include(s => s.User)
            .Include(s => s.Branch)
            .Include(s => s.Items).ThenInclude(i => i.Tamale)
            .Include(s => s.Items).ThenInclude(i => i.Beverage));
        return _mapper.Map<IEnumerable<SaleDto>>(sales);
    }

    public async Task<SaleDto?> GetByIdAsync(int id)
    {
        var sale = await _saleRepository.GetByIdAsync(id, q => q
            .Include(s => s.User)
            .Include(s => s.Branch)
            .Include(s => s.Items).ThenInclude(i => i.Tamale)
            .Include(s => s.Items).ThenInclude(i => i.Beverage));
        return sale is null ? null : _mapper.Map<SaleDto>(sale);
    }
}
