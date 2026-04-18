using GoodHamburger.Core.Entities;
using GoodHamburger.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Infrastructure.Services
{
    public class MenuService : IMenuService
    {
        private readonly List<MenuItem> _menuItems;
        private readonly DiscountRule[] _discountRules;

        public MenuService()
        {
            _menuItems = new List<MenuItem>
        {
            new() { Id = "xburger", Name = "X Burger", Price = 5.00m, Description = "Hambúrguer com queijo", Category = MenuCategory.Sandwich },
            new() { Id = "xegg", Name = "X Egg", Price = 4.50m, Description = "Hambúrguer com ovo e queijo", Category = MenuCategory.Sandwich },
            new() { Id = "xbacon", Name = "X Bacon", Price = 7.00m, Description = "Hambúrguer com bacon e queijo", Category = MenuCategory.Sandwich },
            
            new() { Id = "fries", Name = "Batata Frita", Price = 2.00m, Description = "Batata frita crocante", Category = MenuCategory.SideDish },
            
            new() { Id = "soda", Name = "Refrigerante", Price = 2.50m, Description = "Lata 350ml", Category = MenuCategory.Drink }
        };

            _discountRules = new[]
            {
            new DiscountRule
            {
                Name = "Combo Good",
                Description = "Sanduíche + Batata + Refrigerante",
                Discount = "20%",
                Condition = "Quando pedir os três itens juntos"
            },
            new DiscountRule
            {
                Name = "Promoção Drink",
                Description = "Sanduíche + Refrigerante",
                Discount = "15%",
                Condition = "Quando pedir sanduíche com refrigerante"
            },
            new DiscountRule
            {
                Name = "Promoção Side",
                Description = "Sanduíche + Batata",
                Discount = "10%",
                Condition = "Quando pedir sanduíche com batata"
            }
        };
        }

        public Task<IEnumerable<MenuItem>> GetSandwichesAsync()
            => Task.FromResult(_menuItems.Where(x => x.Category == MenuCategory.Sandwich));

        public Task<IEnumerable<MenuItem>> GetSideDishesAsync()
            => Task.FromResult(_menuItems.Where(x => x.Category == MenuCategory.SideDish));

        public Task<IEnumerable<MenuItem>> GetDrinksAsync()
            => Task.FromResult(_menuItems.Where(x => x.Category == MenuCategory.Drink));

        public Task<MenuItem?> GetMenuItemByIdAsync(string id)
            => Task.FromResult(_menuItems.FirstOrDefault(x => x.Id == id));

        public Task<DiscountRule[]> GetDiscountRulesAsync()
            => Task.FromResult(_discountRules);
    }
}
