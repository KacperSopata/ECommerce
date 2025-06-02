using Microsoft.AspNetCore.Mvc;
using ECommerce.Domain.Model;
using ECommerce.Application.Interfaces;
using ECommerce.Application.Dtos.Order; // użycie istniejącego DTO
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order?>> GetById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateOrderDto dto)
        {
            var order = new Order
            {
                CreatedAt = dto.CreatedAt,
                Products = dto.ProductIds.Select(id => new Product { Id = id }).ToList()
            };

            await _orderService.AddAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Order order)
        {
            if (id != order.Id)
                return BadRequest("ID mismatch");

            await _orderService.UpdateAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _orderService.DeleteAsync(id);
            return NoContent();
        }
    }
}
