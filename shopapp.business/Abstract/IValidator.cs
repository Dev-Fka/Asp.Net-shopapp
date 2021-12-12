using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopapp.business.Abstract
{
    public interface IValidator<T>
    {
        public string? Message { get; set; }
        public bool validation(T entity);
    }
}