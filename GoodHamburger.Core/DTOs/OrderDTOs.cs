using GoodHamburger.Core.Entities;

namespace GoodHamburger.Core.DTOs;

public record CreateOrderRequest(
    SandwichType Sandwich,
    SideDishType? SideDish,
    DrinkType? Drink,
    int? SandwichQuantity = null,
    int? SideDishQuantity = null,
    int? DrinkQuantity = null
);

public record UpdateOrderRequest(
    SandwichType Sandwich,
    SideDishType? SideDish,
    DrinkType? Drink,
    int? SandwichQuantity = null,
    int? SideDishQuantity = null,
    int? DrinkQuantity = null
);

public record OrderResponse(
    Guid Id,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    SandwichType Sandwich,
    SideDishType? SideDish,
    DrinkType? Drink,
    decimal Subtotal,
    decimal Discount,
    decimal Total,
    DiscountType AppliedDiscount
);