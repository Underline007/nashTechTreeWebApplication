using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeWeb.Core.Entities
{
    public class Product
    {
        public int ProductId { get; set; }


        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string? Image { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public List<OrderDetail> OrderDetail { get; set; }
        public List<CartDetail> CartDetail { get; set; }
    }
}
