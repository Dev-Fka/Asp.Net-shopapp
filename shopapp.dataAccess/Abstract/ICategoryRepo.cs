using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity;

namespace shopapp.dataAccess.Abstract
{
    public interface ICategoryRepo : IRepo<Category>
    {
        List<Category> getPopularCategory();

        Category getByIdWithProducts(int id);
        void deleteFromCategory(int productId, int categoryId);
    }
}