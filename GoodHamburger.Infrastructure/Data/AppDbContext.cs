using GoodHamburger.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Order> Orders { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureOrderEntity(modelBuilder);
        ConfigureMenuItemEntity(modelBuilder);

        // modelBuilder.Entity<Order>().HasData(SeedData.GetOrders());
        // modelBuilder.Entity<MenuItem>().HasData(SeedData.GetMenuItems());
    }

    private static void ConfigureOrderEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Sandwich)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(e => e.SideDish)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired(false);

            entity.Property(e => e.Drink)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired(false);

            entity.Property(e => e.AppliedDiscount)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(e => e.Subtotal)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(e => e.Discount)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(e => e.Total)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired(false);

            entity.HasIndex(e => e.CreatedAt)
                .HasDatabaseName("IX_Orders_CreatedAt");

            entity.HasIndex(e => e.Sandwich)
                .HasDatabaseName("IX_Orders_Sandwich");

            entity.HasIndex(e => new { e.CreatedAt, e.Sandwich })
                .HasDatabaseName("IX_Orders_CreatedAt_Sandwich");
        });
    }

    private static void ConfigureMenuItemEntity(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            entity.Property(e => e.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(e => e.Category)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .IsRequired();

            entity.Property(e => e.DisplayOrder)
                .HasDefaultValue(0)
                .IsRequired();

            entity.HasIndex(e => e.Category)
                .HasDatabaseName("IX_MenuItems_Category");

            entity.HasIndex(e => e.IsActive)
                .HasDatabaseName("IX_MenuItems_IsActive");

            entity.HasIndex(e => new { e.Category, e.DisplayOrder })
                .HasDatabaseName("IX_MenuItems_Category_DisplayOrder");

            entity.HasIndex(e => e.Id)
                .IsUnique();
        });
    }
}