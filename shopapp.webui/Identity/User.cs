using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace shopapp.webui.Identity
{
    public class User : IdentityUser
    {

        // ilgili alanlar base sınıftan geliyor. ek tanımlamaları burada yaptık.
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

    }
}