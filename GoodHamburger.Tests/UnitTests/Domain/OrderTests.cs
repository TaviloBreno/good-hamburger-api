using GoodHamburger.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Tests.UnitTests.Domain
{
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
    }
}
