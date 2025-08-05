using AutoMapper;
using CazuelaChapina.API.DTOs;
using CazuelaChapina.API.Models;
using System.Linq;

namespace CazuelaChapina.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateTamaleDto, Tamale>();
        CreateMap<Tamale, TamaleDto>()
            .ForMember(d => d.TamaleType, opt => opt.MapFrom(s => s.TamaleType.Name))
            .ForMember(d => d.Filling, opt => opt.MapFrom(s => s.Filling.Name))
            .ForMember(d => d.Wrapper, opt => opt.MapFrom(s => s.Wrapper.Name))
            .ForMember(d => d.SpiceLevel, opt => opt.MapFrom(s => s.SpiceLevel.Name));

        CreateMap<CreateBeverageDto, Beverage>()
            .ForMember(d => d.BeverageToppings, opt => opt.MapFrom(s => s.ToppingIds.Select(id => new BeverageTopping { ToppingId = id })));
        CreateMap<Beverage, BeverageDto>()
            .ForMember(d => d.Type, opt => opt.MapFrom(s => s.Type.Name))
            .ForMember(d => d.Size, opt => opt.MapFrom(s => s.Size.Name))
            .ForMember(d => d.Sweetener, opt => opt.MapFrom(s => s.Sweetener.Name))
            .ForMember(d => d.Toppings, opt => opt.MapFrom(s => s.BeverageToppings.Select(bt => bt.Topping.Name)));

        CreateMap<ComboItemTamaleDto, ComboItemTamale>();
        CreateMap<ComboItemBeverageDto, ComboItemBeverage>();
        CreateMap<ComboItemTamale, ComboItemTamaleDto>()
            .ForMember(d => d.Tamale, opt => opt.MapFrom(s => s.Tamale));
        CreateMap<ComboItemBeverage, ComboItemBeverageDto>()
            .ForMember(d => d.Beverage, opt => opt.MapFrom(s => s.Beverage));
        CreateMap<CreateComboDto, Combo>();
        CreateMap<Combo, ComboDto>()
            .ForMember(d => d.Season, opt => opt.MapFrom(s => s.Season.ToString()))
            .ForMember(d => d.Total, opt => opt.MapFrom(s =>
                s.Tamales.Sum(t => t.Tamale.Price * t.Quantity) +
                s.Beverages.Sum(b => b.Beverage.Price * b.Quantity)));

        CreateMap<CreateInventoryItemDto, InventoryItem>();
        CreateMap<InventoryItem, InventoryItemDto>();

        CreateMap<CreateInventoryEntryDto, InventoryEntry>();
        CreateMap<InventoryEntry, InventoryEntryDto>()
            .ForMember(d => d.ItemName, opt => opt.MapFrom(s => s.Item!.Name));

        CreateMap<PermissionDto, Permission>();
        CreateMap<Permission, PermissionDto>();

        CreateMap<User, UserDto>()
            .ForMember(d => d.Branch, opt => opt.MapFrom(s => s.Branch != null ? s.Branch.Name : null))
            .ForMember(d => d.Role, opt => opt.MapFrom(s => s.Role != null ? s.Role.Name : null));
        CreateMap<CreateUserDto, User>()
            .ForMember(d => d.PasswordHash, opt => opt.MapFrom(s => BCrypt.Net.BCrypt.HashPassword(s.Password)));
        CreateMap<UpdateUserDto, User>();

        CreateMap<Role, RoleDto>();
        CreateMap<CreateRoleDto, Role>();
        CreateMap<UpdateRoleDto, Role>();

        CreateMap<Module, ModuleDto>();
        CreateMap<CreateModuleDto, Module>();
        CreateMap<UpdateModuleDto, Module>();

        CreateMap<CreateBranchDto, Branch>();
        CreateMap<Branch, BranchDto>();

        CreateMap<CreateTamaleTypeDto, TamaleType>();
        CreateMap<UpdateTamaleTypeDto, TamaleType>();
        CreateMap<TamaleType, TamaleTypeDto>();
        CreateMap<CreateFillingDto, Filling>();
        CreateMap<UpdateFillingDto, Filling>();
        CreateMap<Filling, FillingDto>();
        CreateMap<CreateWrapperDto, Wrapper>();
        CreateMap<UpdateWrapperDto, Wrapper>();
        CreateMap<Wrapper, WrapperDto>();
        CreateMap<CreateSpiceLevelDto, SpiceLevel>();
        CreateMap<UpdateSpiceLevelDto, SpiceLevel>();
        CreateMap<SpiceLevel, SpiceLevelDto>();
        CreateMap<CreateBeverageTypeDto, BeverageType>();
        CreateMap<UpdateBeverageTypeDto, BeverageType>();
        CreateMap<BeverageType, BeverageTypeDto>();
        CreateMap<CreateBeverageSizeDto, BeverageSize>();
        CreateMap<UpdateBeverageSizeDto, BeverageSize>();
        CreateMap<BeverageSize, BeverageSizeDto>();
        CreateMap<CreateSweetenerDto, Sweetener>();
        CreateMap<UpdateSweetenerDto, Sweetener>();
        CreateMap<Sweetener, SweetenerDto>();
        CreateMap<CreateToppingDto, Topping>();
        CreateMap<UpdateToppingDto, Topping>();
        CreateMap<Topping, ToppingDto>();

        CreateMap<SaleItem, SaleItemDto>();
        CreateMap<Sale, SaleDto>()
            .ForMember(d => d.User, opt => opt.MapFrom(s => s.User.Username));
    }
}
