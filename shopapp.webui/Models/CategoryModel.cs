using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity;

namespace shopapp.webui.Models
{
    public class CategoryModel
    {
        [Required(ErrorMessage = "Url alanı boş geçilemez!")]
        public string? Url { get; set; }
        [Required(ErrorMessage = "Name alanı boş geçilemez!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Geçersiz Name yapılandırması!")]
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public List<Product>? products { get; set; }
    }
}