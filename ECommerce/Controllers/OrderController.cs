using ECommerce.Application.Dtos.Order;
using ECommerce.Application.Dtos.Product;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrdersController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            var result = orders.Select(order => new OrderDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                Products = order.Products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    IsAvailable = p.IsAvailable
                }).ToList()
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound();

            var dto = new OrderDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                Products = order.Products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    IsAvailable = p.IsAvailable
                }).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateOrderDto dto)
        {
            // pobierz produkty po ID z repozytorium (żeby mieć pełne encje)
            var products = await _productService.GetByIdsAsync(dto.ProductIds);

            if (products.Count != dto.ProductIds.Count)
                return BadRequest("Niektóre produkty nie istnieją.");

            var order = new Order
            {
                CreatedAt = dto.CreatedAt.ToUniversalTime(), // PostgreSQL wymaga UTC!
                Products = products
            };

            await _orderService.AddAsync(order);

            var result = new OrderDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                Products = order.Products.Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    IsAvailable = p.IsAvailable
                }).ToList()
            };

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, result);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] CreateOrderDto dto)
        {
            var products = await _orderService.GetProductsByIdsAsync(dto.ProductIds);

            var order = new Order
            {
                Id = id,
                CreatedAt = dto.CreatedAt.ToUniversalTime(),
                Products = products
            };

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
