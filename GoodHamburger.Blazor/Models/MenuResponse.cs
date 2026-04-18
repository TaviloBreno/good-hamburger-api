namespace GoodHamburger.Blazor.Models;

public class MenuResponse
{
    public List<MenuItem> Sandwiches { get; set; } = new();
    public List<MenuItem> SideDishes { get; set; } = new();
    public List<MenuItem> Drinks { get; set; } = new();
    public List<DiscountRule> DiscountRules { get; set; } = new();
}

public class MenuItem
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public class DiscountRule
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Discount { get; set; } = string.Empty;
    public string Condition { get; set; } = string.Empty;
}