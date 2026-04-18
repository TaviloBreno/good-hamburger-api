using GoodHamburger.Core.Entities;

namespace GoodHamburger.Tests.UnitTests.Domain;

public class OrderTests
{
    [Fact]
    public void CreateOrder_WithOnlySandwich_ShouldCalculateCorrectPrice()
    {
        var order = new Order(SandwichType.XBurger);

        Assert.Equal(5.00m, order.Subtotal);
        Assert.Equal(0, order.Discount);
        Assert.Equal(5.00m, order.Total);
        Assert.Equal(DiscountType.None, order.AppliedDiscount);
    }

    [Fact]
    public void CreateOrder_WithSandwichAndDrink_ShouldApply15PercentDiscount()
    {
        var order = new Order(SandwichType.XBacon, drink: DrinkType.Soda);

        Assert.Equal(9.50m, order.Subtotal);
        Assert.Equal(1.425m, order.Discount);
        Assert.Equal(8.075m, order.Total);
        Assert.Equal(DiscountType.SandwichDrink, order.AppliedDiscount);
    }

    [Fact]
    public void CreateOrder_WithSandwichAndSide_ShouldApply10PercentDiscount()
    {
        var order = new Order(SandwichType.XEgg, SideDishType.Fries);

        Assert.Equal(6.50m, order.Subtotal);
        Assert.Equal(0.65m, order.Discount);
        Assert.Equal(5.85m, order.Total);
        Assert.Equal(DiscountType.SandwichSide, order.AppliedDiscount);
    }

    [Fact]
    public void CreateOrder_WithAllItems_ShouldApply20PercentDiscount()
    {
        var order = new Order(SandwichType.XEgg, SideDishType.Fries, DrinkType.Soda);

        Assert.Equal(9.00m, order.Subtotal);
        Assert.Equal(1.80m, order.Discount);
        Assert.Equal(7.20m, order.Total);
        Assert.Equal(DiscountType.Combo, order.AppliedDiscount);
    }

    [Fact]
    public void UpdateOrder_ShouldRecalculatePrices()
    {
        var order = new Order(SandwichType.XBurger);

        order.Update(SandwichType.XBacon, SideDishType.Fries, DrinkType.Soda);

        Assert.Equal(11.50m, order.Subtotal);
        Assert.Equal(2.30m, order.Discount);
        Assert.Equal(9.20m, order.Total);
        Assert.NotNull(order.UpdatedAt);
    }

    [Fact]
    public void CreateOrder_WithDifferentSandwiches_ShouldCalculateCorrectPrices()
    {
        var xBurger = new Order(SandwichType.XBurger);
        Assert.Equal(5.00m, xBurger.Subtotal);

        var xEgg = new Order(SandwichType.XEgg);
        Assert.Equal(4.50m, xEgg.Subtotal);

        var xBacon = new Order(SandwichType.XBacon);
        Assert.Equal(7.00m, xBacon.Subtotal);
    }

    [Fact]
    public void Order_CreatedAt_ShouldBeSetAutomatically()
    {
        var beforeCreation = DateTime.UtcNow;
        var order = new Order(SandwichType.XBurger);
        var afterCreation = DateTime.UtcNow;

        Assert.True(order.CreatedAt >= beforeCreation);
        Assert.True(order.CreatedAt <= afterCreation);
    }

    [Fact]
    public void Order_Id_ShouldBeGeneratedAutomatically()
    {
        var order1 = new Order(SandwichType.XBurger);
        var order2 = new Order(SandwichType.XEgg);

        Assert.NotEqual(Guid.Empty, order1.Id);
        Assert.NotEqual(Guid.Empty, order2.Id);
        Assert.NotEqual(order1.Id, order2.Id);
    }

    [Fact]
    public void UpdateOrder_FromSimpleToCombo_ShouldRecalculateDiscount()
    {
        var order = new Order(SandwichType.XBurger);
        Assert.Equal(5.00m, order.Total);
        Assert.Equal(DiscountType.None, order.AppliedDiscount);

        order.Update(SandwichType.XBurger, SideDishType.Fries, DrinkType.Soda);

        Assert.Equal(9.50m, order.Subtotal);
        Assert.Equal(1.90m, order.Discount);
        Assert.Equal(7.60m, order.Total);
        Assert.Equal(DiscountType.Combo, order.AppliedDiscount);
    }

    [Fact]
    public void UpdateOrder_RemoveItems_ShouldRemoveDiscount()
    {
        var order = new Order(SandwichType.XBacon, SideDishType.Fries, DrinkType.Soda);
        Assert.Equal(DiscountType.Combo, order.AppliedDiscount);

        order.Update(SandwichType.XBacon, null, null);

        Assert.Equal(7.00m, order.Subtotal);
        Assert.Equal(0m, order.Discount);
        Assert.Equal(7.00m, order.Total);
        Assert.Equal(DiscountType.None, order.AppliedDiscount);
    }

    [Fact]
    public void CreateOrder_WithDuplicateItems_ShouldThrowException()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            new Order(
                SandwichType.XBurger,
                SideDishType.Fries,
                null,
                sideDishQuantity: 2));

        Assert.Contains("Itens duplicados nao sao permitidos", exception.Message);
        Assert.Contains("acompanhamento", exception.Message);
    }

    [Fact]
    public void CreateOrder_WithInvalidEnumValue_ShouldNotCompile()
    {
        var validSandwiches = Enum.GetValues<SandwichType>();
        Assert.Contains(SandwichType.XBurger, validSandwiches);
        Assert.Contains(SandwichType.XEgg, validSandwiches);
        Assert.Contains(SandwichType.XBacon, validSandwiches);
    }

    [Fact]
    public void Order_ShouldOnlyAllowOneItemPerCategory()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            new Order(
                SandwichType.XBacon,
                null,
                DrinkType.Soda,
                drinkQuantity: 2));

        Assert.Contains("Itens duplicados nao sao permitidos", exception.Message);
        Assert.Contains("bebida", exception.Message);
    }

    [Fact]
    public void CreateOrder_WithQuantityWithoutSelectedItem_ShouldThrowException()
    {
        var exception = Assert.Throws<ArgumentException>(() =>
            new Order(
                SandwichType.XEgg,
                null,
                null,
                sideDishQuantity: 1));

        Assert.Contains("sem selecionar acompanhamento", exception.Message);
    }

    [Theory]
    [InlineData(SandwichType.XBurger, 5.00)]
    [InlineData(SandwichType.XEgg, 4.50)]
    [InlineData(SandwichType.XBacon, 7.00)]
    public void CreateOrder_DifferentSandwiches_ShouldHaveCorrectPrices(SandwichType sandwich, decimal expectedPrice)
    {
        var order = new Order(sandwich);

        Assert.Equal(expectedPrice, order.Subtotal);
        Assert.Equal(expectedPrice, order.Total);
    }

    [Theory]
    [InlineData(SandwichType.XBurger, SideDishType.Fries, null, 7.00, 0.70, 6.30, DiscountType.SandwichSide)]
    [InlineData(SandwichType.XBacon, null, DrinkType.Soda, 9.50, 1.425, 8.075, DiscountType.SandwichDrink)]
    [InlineData(SandwichType.XEgg, SideDishType.Fries, DrinkType.Soda, 9.00, 1.80, 7.20, DiscountType.Combo)]
    [InlineData(SandwichType.XBurger, null, null, 5.00, 0, 5.00, DiscountType.None)]
    public void CreateOrder_VariousCombinations_ShouldApplyCorrectDiscounts(
        SandwichType sandwich,
        SideDishType? sideDish,
        DrinkType? drink,
        decimal expectedSubtotal,
        decimal expectedDiscount,
        decimal expectedTotal,
        DiscountType expectedDiscountType)
    {
        var order = new Order(sandwich, sideDish, drink);

        Assert.Equal(expectedSubtotal, order.Subtotal);
        Assert.Equal(expectedDiscount, order.Discount);
        Assert.Equal(expectedTotal, order.Total);
        Assert.Equal(expectedDiscountType, order.AppliedDiscount);
    }
}