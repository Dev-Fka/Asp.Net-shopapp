using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.entity
{
    public class Order
    {
        public int orderId { get; set; }
        public List<Product>? products { get; set; }
    }
}