using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.webui.Models
{
    public class LoginModel
    {
        [Required]
        public string? userName { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string? passWord { get; set; }
        public string? returnUrl { get; set; }
    }
}