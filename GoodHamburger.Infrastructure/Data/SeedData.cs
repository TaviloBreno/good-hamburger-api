using GoodHamburger.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Data;

public static class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MenuItem>().HasData(
            new MenuItem { Id = "xburger", Name = "X Burger", Price = 5.00m, Description = "Hambúrguer 150g com queijo derretido", Category = MenuCategory.Sandwich, IsActive = true, DisplayOrder = 1 },
            new MenuItem { Id = "xegg", Name = "X Egg", Price = 4.50m, Description = "Hambúrguer com ovo frito e queijo", Category = MenuCategory.Sandwich, IsActive = true, DisplayOrder = 2 },
            new MenuItem { Id = "xbacon", Name = "X Bacon", Price = 7.00m, Description = "Hambúrguer com bacon crocante e queijo", Category = MenuCategory.Sandwich, IsActive = true, DisplayOrder = 3 },
            new MenuItem { Id = "fries", Name = "Batata Frita", Price = 2.00m, Description = "Batata frita crocante (150g)", Category = MenuCategory.SideDish, IsActive = true, DisplayOrder = 1 },
            new MenuItem { Id = "soda", Name = "Refrigerante", Price = 2.50m, Description = "Lata 350ml", Category = MenuCategory.Drink, IsActive = true, DisplayOrder = 1 }
        );

        modelBuilder.Entity<Order>().HasData(
            new
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                UpdatedAt = (DateTime?)null,
                Sandwich = SandwichType.XBurger,
                SideDish = (SideDishType?)SideDishType.Fries,
                Drink = (DrinkType?)DrinkType.Soda,
                Subtotal = 9.50m,
                Discount = 1.90m,
                Total = 7.60m,
                AppliedDiscount = DiscountType.Combo
            },
            new
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                CreatedAt = DateTime.UtcNow.AddDays(-3),
                UpdatedAt = (DateTime?)null,
                Sandwich = SandwichType.XBacon,
                SideDish = (SideDishType?)null,
                Drink = (DrinkType?)DrinkType.Soda,
                Subtotal = 9.50m,
                Discount = 1.425m,
                Total = 8.075m,
                AppliedDiscount = DiscountType.SandwichDrink
            },
            new
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                UpdatedAt = (DateTime?)null,
                Sandwich = SandwichType.XEgg,
                SideDish = (SideDishType?)SideDishType.Fries,
                Drink = (DrinkType?)null,
                Subtotal = 6.50m,
                Discount = 0.65m,
                Total = 5.85m,
                AppliedDiscount = DiscountType.SandwichSide
            },
            new
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                UpdatedAt = (DateTime?)null,
                Sandwich = SandwichType.XBurger,
                SideDish = (SideDishType?)null,
                Drink = (DrinkType?)null,
                Subtotal = 5.00m,
                Discount = 0m,
                Total = 5.00m,
                AppliedDiscount = DiscountType.None
            }
        );
    }
}