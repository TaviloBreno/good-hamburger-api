using GoodHamburger.Core.Entities;

namespace GoodHamburger.Core.Interfaces;

public interface IMenuService
{
    Task<IEnumerable<MenuItem>> GetSandwichesAsync();
    Task<IEnumerable<MenuItem>> GetSideDishesAsync();
    Task<IEnumerable<MenuItem>> GetDrinksAsync();
    Task<MenuItem?> GetMenuItemByIdAsync(string id);
    Task<DiscountRule[]> GetDiscountRulesAsync();
}

public record DiscountRule
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Discount { get; init; } = string.Empty;
    public string Condition { get; init; } = string.Empty;
}