using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity;

namespace shopapp.webui.Models
{
    public class CategoryModel
    {
        public string? Url { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public List<Product>? products { get; set; }
    }
}