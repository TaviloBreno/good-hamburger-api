using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MenuController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetMenu()
        {
            var menu = new
            {
                Sandwiches = new[]
                {
                new { Id = "xburger", Name = "X Burger", Price = 5.00m, Description = "Hambúrguer com queijo" },
                new { Id = "xegg", Name = "X Egg", Price = 4.50m, Description = "Hambúrguer com ovo e queijo" },
                new { Id = "xbacon", Name = "X Bacon", Price = 7.00m, Description = "Hambúrguer com bacon e queijo" }
            },
                SideDishes = new[]
                {
                new { Id = "fries", Name = "Batata Frita", Price = 2.00m, Description = "Batata frita crocante" }
            },
                Drinks = new[]
                {
                new { Id = "soda", Name = "Refrigerante", Price = 2.50m, Description = "Lata 350ml" }
            },
                DiscountRules = new[]
                {
                new {
                    Name = "Combo Good",
                    Description = "Sanduíche + Batata + Refrigerante",
                    Discount = "20%",
                    Condition = "Quando pedir os três itens juntos"
                },
                new {
                    Name = "Promoção Drink",
                    Description = "Sanduíche + Refrigerante",
                    Discount = "15%",
                    Condition = "Quando pedir sanduíche com refrigerante"
                },
                new {
                    Name = "Promoção Side",
                    Description = "Sanduíche + Batata",
                    Discount = "10%",
                    Condition = "Quando pedir sanduíche com batata"
                }
            }
            };

            return Ok(menu);
        }
    }
}
