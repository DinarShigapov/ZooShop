using Cashier.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier.Classes
{
    public class ProductQuantity
    {
        public ProductQuantity(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
