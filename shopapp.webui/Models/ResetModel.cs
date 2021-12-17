using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.webui.Models
{
    public class ResetModel
    {
        [Required]
        public string? token { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? eMail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? password { get; set; }
    }
}