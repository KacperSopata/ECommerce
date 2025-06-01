using ECommerce.Application.Dtos.Product;

namespace ECommerce.Application.Dtos.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ProductDto> Products { get; set; } = new();
    }
}