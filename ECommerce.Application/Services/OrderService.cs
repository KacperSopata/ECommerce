using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Application.Interfaces;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Model;

namespace ECommerce.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Task<IEnumerable<Order>> GetAllAsync()
        {
            return _orderRepository.GetAllAsync();
        }

        public Task<Order?> GetByIdAsync(int id)
        {
            return _orderRepository.GetByIdAsync(id);
        }

        public Task AddAsync(Order order)
        {
            return _orderRepository.AddAsync(order);
        }

        public Task<Order> UpdateAsync(Order order)
        {
            return _orderRepository.UpdateAsync(order);
        }

        public Task DeleteAsync(int id)
        {
            return _orderRepository.DeleteAsync(id);
        }
    }
}
