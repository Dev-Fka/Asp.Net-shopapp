using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity;

namespace shopapp.webui.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string? Url { get; set; }
        //[Required(ErrorMessage = "Boş bırakılamaz!")]
        public string? Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
        //[Display(Name = "Image Url")]
        public string? ImageUrl { get; set; }
        public bool ısHome { get; set; }
        public List<Category>? selectedCategories { get; set; }
    }
}