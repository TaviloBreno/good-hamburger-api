using GoodHamburger.Core.Entities;

namespace GoodHamburger.Core.DTOs;

public record CreateOrderRequest(
    SandwichType Sandwich,
    SideDishType? SideDish,
    DrinkType? Drink
);

public record UpdateOrderRequest(
    SandwichType Sandwich,
    SideDishType? SideDish,
    DrinkType? Drink
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