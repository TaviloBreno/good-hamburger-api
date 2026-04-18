using GoodHamburger.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Sandwich)
                .HasConversion<string>()
                .HasMaxLength(20);
                
            entity.Property(e => e.SideDish)
                .HasConversion<string>()
                .HasMaxLength(20);
                
            entity.Property(e => e.Drink)
                .HasConversion<string>()
                .HasMaxLength(20);
                
            entity.Property(e => e.AppliedDiscount)
                .HasConversion<string>()
                .HasMaxLength(20);
                
            entity.Property(e => e.Subtotal)
                .HasPrecision(18, 2);
                
            entity.Property(e => e.Discount)
                .HasPrecision(18, 2);
                
            entity.Property(e => e.Total)
                .HasPrecision(18, 2);
                
            entity.HasIndex(e => e.CreatedAt);
        });
    }
}