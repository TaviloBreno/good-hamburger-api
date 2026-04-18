using GoodHamburger.Core.DTOs;
using GoodHamburger.Core.Entities;
using GoodHamburger.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderRepository orderRepository, ILogger<OrdersController> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderRepository.GetAllAsync();
            return Ok(orders.Select(MapToResponse));
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound(new { message = $"Order with ID {id} not found" });

            return Ok(MapToResponse(order));
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            try
            {
                var order = new Order(
                    request.Sandwich,
                    request.SideDish,
                    request.Drink,
                    request.SandwichQuantity,
                    request.SideDishQuantity,
                    request.DrinkQuantity);
                await _orderRepository.AddAsync(order);

                _logger.LogInformation("Order {OrderId} created successfully", order.Id);

                return CreatedAtAction(nameof(GetById), new { id = order.Id }, MapToResponse(order));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOrderRequest request)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound(new { message = $"Order with ID {id} not found" });

            try
            {
                order.Update(
                    request.Sandwich,
                    request.SideDish,
                    request.Drink,
                    request.SandwichQuantity,
                    request.SideDishQuantity,
                    request.DrinkQuantity);
                await _orderRepository.UpdateAsync(order);

                _logger.LogInformation("Order {OrderId} updated successfully", order.Id);

                return Ok(MapToResponse(order));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound(new { message = $"Order with ID {id} not found" });

            await _orderRepository.DeleteAsync(order);

            _logger.LogInformation("Order {OrderId} deleted successfully", order.Id);

            return NoContent();
        }

        private static OrderResponse MapToResponse(Order order) => new(
            order.Id,
            order.CreatedAt,
            order.UpdatedAt,
            order.Sandwich,
            order.SideDish,
            order.Drink,
            order.Subtotal,
            order.Discount,
            order.Total,
            order.AppliedDiscount
        );
    }
}
