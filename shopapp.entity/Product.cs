using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity.obj;

namespace shopapp.entity
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? Url { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool ısHome { get; set; }
        public List<ProductCategory>? ProductCategories { get; set; }
    }
}