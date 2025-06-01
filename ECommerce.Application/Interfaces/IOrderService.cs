using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Domain.Model;

namespace ECommerce.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task AddAsync(Order order);
        Task<Order> UpdateAsync(Order order);
        Task DeleteAsync(int id);
    }
}
