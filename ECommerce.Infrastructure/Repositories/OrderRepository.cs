using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ECommerceDbContext _context;

        public OrderRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
            => await _context.Orders.Include(o => o.Products).ToListAsync();

        public async Task<Order?> GetByIdAsync(int id)
            => await _context.Orders.Include(o => o.Products).FirstOrDefaultAsync(o => o.Id == id);

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            var existingOrder = await _context.Orders
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.Id == order.Id);

            if (existingOrder == null)
                throw new KeyNotFoundException($"Order with ID {order.Id} not found");

            existingOrder.CreatedAt = order.CreatedAt;
            existingOrder.Products.Clear();

            foreach (var product in order.Products)
                _context.Attach(product); // ważne

            existingOrder.Products = order.Products;

            await _context.SaveChangesAsync();
            return existingOrder;
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
