namespace GoodHamburger.Core.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public SandwichType Sandwich { get; private set; }
    public SideDishType? SideDish { get; private set; }
    public DrinkType? Drink { get; private set; }
    public decimal Subtotal { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Total { get; private set; }
    public DiscountType AppliedDiscount { get; private set; }

    private Order() { } // For EF Core

    public Order(SandwichType sandwich, SideDishType? sideDish = null, DrinkType? drink = null)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        
        SetItems(sandwich, sideDish, drink);
        CalculatePrices();
    }

    public void Update(SandwichType sandwich, SideDishType? sideDish, DrinkType? drink)
    {
        SetItems(sandwich, sideDish, drink);
        UpdatedAt = DateTime.UtcNow;
        CalculatePrices();
    }

    private void SetItems(SandwichType sandwich, SideDishType? sideDish, DrinkType? drink)
    {
        ValidateDuplicateItems(sideDish, drink);
        
        Sandwich = sandwich;
        SideDish = sideDish;
        Drink = drink;
    }

    private void ValidateDuplicateItems(SideDishType? sideDish, DrinkType? drink)
    {
        // No duplicate validation needed as each item can only appear onc
        // This method is a placeholder for future validations
        
    }

    private void CalculatePrices()
    {
        Subtotal = CalculateSubtotal();
        (Discount, AppliedDiscount) = CalculateDiscount();
        Total = Subtotal - Discount;
    }

    private decimal CalculateSubtotal()
    {
        decimal subtotal = GetSandwichPrice(Sandwich);
        
        if (SideDish.HasValue)
            subtotal += GetSideDishPrice(SideDish.Value);
            
        if (Drink.HasValue)
            subtotal += GetDrinkPrice(Drink.Value);
            
        return subtotal;
    }

    private (decimal discount, DiscountType type) CalculateDiscount()
    {
        bool hasSideDish = SideDish.HasValue;
        bool hasDrink = Drink.HasValue;

        if (hasSideDish && hasDrink)
            return (Subtotal * 0.20m, DiscountType.Combo);
        
        if (hasDrink)
            return (Subtotal * 0.15m, DiscountType.SandwichDrink);
        
        if (hasSideDish)
            return (Subtotal * 0.10m, DiscountType.SandwichSide);
        
        return (0, DiscountType.None);
    }

    private decimal GetSandwichPrice(SandwichType sandwich) => sandwich switch
    {
        SandwichType.XBurger => 5.00m,
        SandwichType.XEgg => 4.50m,
        SandwichType.XBacon => 7.00m,
        _ => throw new ArgumentException($"Invalid sandwich type: {sandwich}")
    };

    private decimal GetSideDishPrice(SideDishType sideDish) => sideDish switch
    {
        SideDishType.Fries => 2.00m,
        _ => throw new ArgumentException($"Invalid side dish: {sideDish}")
    };

    private decimal GetDrinkPrice(DrinkType drink) => drink switch
    {
        DrinkType.Soda => 2.50m,
        _ => throw new ArgumentException($"Invalid drink: {drink}")
    };
}

public enum SandwichType
{
    XBurger,
    XEgg,
    XBacon
}

public enum SideDishType
{
    Fries
}

public enum DrinkType
{
    Soda
}

public enum DiscountType
{
    None,
    SandwichSide,
    SandwichDrink,
    Combo
}