using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity;

namespace shopapp.business.Abstract
{
    public interface ICategoryService
    {
        Category getById(int id);
        List<Category> getAll();
        void create(Category entity);
        void update(Category entity);
        void delete(Category entity);
        Category getByIdWithProducts(int id);
        void deleteFromCategory(int productId, int categoryId);
    }
}