using GoodHamburger.Core.Entities;

namespace GoodHamburger.Infrastructure.Data;

public static class SeedData
{
    public static List<Order> GetOrders()
    {
        var orders = new List<Order>();

        var order1 = new Order(SandwichType.XBurger, SideDishType.Fries, DrinkType.Soda);
        SetPrivateProperty(order1, "Id", Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"));
        SetPrivateProperty(order1, "CreatedAt", DateTime.UtcNow.AddDays(-5));
        orders.Add(order1);

        var order2 = new Order(SandwichType.XBacon, null, DrinkType.Soda);
        SetPrivateProperty(order2, "Id", Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f23456789012"));
        SetPrivateProperty(order2, "CreatedAt", DateTime.UtcNow.AddDays(-3));
        orders.Add(order2);

        var order3 = new Order(SandwichType.XEgg, SideDishType.Fries, null);
        SetPrivateProperty(order3, "Id", Guid.Parse("c3d4e5f6-a7b8-9012-cdef-345678901234"));
        SetPrivateProperty(order3, "CreatedAt", DateTime.UtcNow.AddDays(-2));
        orders.Add(order3);

        var order4 = new Order(SandwichType.XBurger);
        SetPrivateProperty(order4, "Id", Guid.Parse("d4e5f6a7-b8c9-0123-defg-456789012345"));
        SetPrivateProperty(order4, "CreatedAt", DateTime.UtcNow.AddDays(-1));
        orders.Add(order4);

        return orders;
    }

    public static List<MenuItem> GetMenuItems()
    {
        return new List<MenuItem>
        {
            new()
            {
                Id = "xburger",
                Name = "X Burger",
                Price = 5.00m,
                Description = "Hambúrguer 150g com queijo derretido",
                Category = MenuCategory.Sandwich,
                IsActive = true,
                DisplayOrder = 1
            },
            new()
            {
                Id = "xegg",
                Name = "X Egg",
                Price = 4.50m,
                Description = "Hambúrguer com ovo frito e queijo",
                Category = MenuCategory.Sandwich,
                IsActive = true,
                DisplayOrder = 2
            },
            new()
            {
                Id = "xbacon",
                Name = "X Bacon",
                Price = 7.00m,
                Description = "Hambúrguer com bacon crocante e queijo",
                Category = MenuCategory.Sandwich,
                IsActive = true,
                DisplayOrder = 3
            },
            new()
            {
                Id = "fries",
                Name = "Batata Frita",
                Price = 2.00m,
                Description = "Batata frita crocante (150g)",
                Category = MenuCategory.SideDish,
                IsActive = true,
                DisplayOrder = 1
            },
            new()
            {
                Id = "soda",
                Name = "Refrigerante",
                Price = 2.50m,
                Description = "Lata 350ml",
                Category = MenuCategory.Drink,
                IsActive = true,
                DisplayOrder = 1
            }
        };
    }

    private static void SetPrivateProperty(object obj, string propertyName, object value)
    {
        var property = obj.GetType().GetProperty(propertyName,
            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

        if (property != null && property.CanWrite)
        {
            property.SetValue(obj, value);
        }
    }
}