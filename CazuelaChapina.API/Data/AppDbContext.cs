using Microsoft.EntityFrameworkCore;
using CazuelaChapina.API.Models;

namespace CazuelaChapina.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Tamale> Tamales => Set<Tamale>();
        public DbSet<Beverage> Beverages => Set<Beverage>();
        public DbSet<Combo> Combos => Set<Combo>();
        public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
        public DbSet<InventoryEntry> InventoryEntries => Set<InventoryEntry>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<Module> Modules => Set<Module>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<Device> Devices => Set<Device>();

        public DbSet<TamaleType> TamaleTypes => Set<TamaleType>();
        public DbSet<Filling> Fillings => Set<Filling>();
        public DbSet<Wrapper> Wrappers => Set<Wrapper>();
        public DbSet<SpiceLevel> SpiceLevels => Set<SpiceLevel>();
        public DbSet<BeverageType> BeverageTypes => Set<BeverageType>();
        public DbSet<BeverageSize> BeverageSizes => Set<BeverageSize>();
        public DbSet<Sweetener> Sweeteners => Set<Sweetener>();
        public DbSet<Topping> Toppings => Set<Topping>();
        public DbSet<BeverageTopping> BeverageToppings => Set<BeverageTopping>();
        public DbSet<ComboItemTamale> ComboItemTamales => Set<ComboItemTamale>();
        public DbSet<ComboItemBeverage> ComboItemBeverages => Set<ComboItemBeverage>();
        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<SaleItem> SaleItems => Set<SaleItem>();
        public DbSet<TamaleIngredient> TamaleIngredients => Set<TamaleIngredient>();
        public DbSet<BeverageIngredient> BeverageIngredients => Set<BeverageIngredient>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<InventoryItem>().ToTable("Inventory");
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<Permission>()
                .HasIndex(p => new { p.RoleId, p.ModuleId })
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.Branch)
                .WithMany(b => b.Users)
                .HasForeignKey(u => u.BranchId);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Branch)
                .WithMany(b => b.Sales)
                .HasForeignKey(s => s.BranchId);

            modelBuilder.Entity<Module>().HasData(
                new Module { Id = 1, Name = "Tamales" },
                new Module { Id = 2, Name = "Beverages" },
                new Module { Id = 3, Name = "Combos" },
                new Module { Id = 4, Name = "Inventory" },
                new Module { Id = 5, Name = "Dashboard" },
                new Module { Id = 6, Name = "Users" },
                new Module { Id = 7, Name = "Sales" }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "admin", Description = "Administrator" }
            );

            modelBuilder.Entity<BeverageTopping>().HasKey(bt => new { bt.BeverageId, bt.ToppingId });
            modelBuilder.Entity<ComboItemTamale>().HasKey(ct => new { ct.ComboId, ct.TamaleId });
            modelBuilder.Entity<ComboItemBeverage>().HasKey(cb => new { cb.ComboId, cb.BeverageId });

            modelBuilder.Entity<SaleItem>()
                .HasOne(si => si.Tamale)
                .WithMany()
                .HasForeignKey(si => si.TamaleId);

            modelBuilder.Entity<SaleItem>()
                .HasOne(si => si.Beverage)
                .WithMany()
                .HasForeignKey(si => si.BeverageId);

            modelBuilder.Entity<Device>()
                .HasIndex(d => d.Token)
                .IsUnique();
            modelBuilder.Entity<Device>()
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId);
        }
    }
}
