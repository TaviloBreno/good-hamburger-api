using GoodHamburger.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly ILogger<MenuController> _logger;

        public MenuController(IMenuService menuService, ILogger<MenuController> logger)
        {
            _menuService = menuService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(MenuResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMenu()
        {
            var sandwiches = await _menuService.GetSandwichesAsync();
            var sideDishes = await _menuService.GetSideDishesAsync();
            var drinks = await _menuService.GetDrinksAsync();
            var discountRules = await _menuService.GetDiscountRulesAsync();

            var menu = new MenuResponse
            {
                Sandwiches = sandwiches.Select(s => new MenuItemDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                    Description = s.Description
                }),
                SideDishes = sideDishes.Select(s => new MenuItemDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price,
                    Description = s.Description
                }),
                Drinks = drinks.Select(d => new MenuItemDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Price = d.Price,
                    Description = d.Description
                }),
                DiscountRules = discountRules
            };

            _logger.LogInformation("Menu retrieved successfully");
            return Ok(menu);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MenuItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMenuItem(string id)
        {
            var item = await _menuService.GetMenuItemByIdAsync(id);

            if (item == null)
                return NotFound(new { message = $"Menu item with ID '{id}' not found" });

            return Ok(new MenuItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                Description = item.Description
            });
        }
    }

    public class MenuResponse
    {
        public IEnumerable<MenuItemDto> Sandwiches { get; set; } = new List<MenuItemDto>();
        public IEnumerable<MenuItemDto> SideDishes { get; set; } = new List<MenuItemDto>();
        public IEnumerable<MenuItemDto> Drinks { get; set; } = new List<MenuItemDto>();
        public IEnumerable<DiscountRule> DiscountRules { get; set; } = new List<DiscountRule>();
    }

    public class MenuItemDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
