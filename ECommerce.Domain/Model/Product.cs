using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Model
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Price { get; set; }
        public string Description { get; set; } = null!;
        public bool IsAvailable { get; set; } = true;  
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
