namespace GoodHamburger.Core.Entities;

public class MenuItem
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public MenuCategory Category { get; set; }
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }
}

public enum MenuCategory
{
    Sandwich,
    SideDish,
    Drink
}