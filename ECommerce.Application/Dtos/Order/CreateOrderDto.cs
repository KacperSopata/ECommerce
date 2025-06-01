namespace ECommerce.Application.Dtos.Order
{
    public class CreateOrderDto
    {
        public List<int> ProductIds { get; set; } = new();
    }
}