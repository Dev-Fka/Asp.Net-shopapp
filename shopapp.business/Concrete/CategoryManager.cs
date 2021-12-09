using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.business.Abstract;
using shopapp.dataAccess.Abstract;
using shopapp.entity;

namespace shopapp.business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryRepo categoryRepo;

        public CategoryManager(ICategoryRepo _categoryRepo)
        {
            categoryRepo = _categoryRepo;
        }

        public void create(Category entity)
        {
            categoryRepo.create(entity);
        }

        public void delete(Category entity)
        {
            categoryRepo.delete(entity);
        }

        public void deleteFromCategory(int productId, int categoryId)
        {
            categoryRepo.deleteFromCategory(productId, categoryId);
        }

        public List<Category> getAll()
        {
            return categoryRepo.getAll();
        }

        public Category getById(int id)
        {
            return categoryRepo.getById(id);
        }

        public Category getByIdWithProducts(int id)
        {
            return categoryRepo.getByIdWithProducts(id);
        }

        public void update(Category entity)
        {
            categoryRepo.update(entity);
        }
    }
}