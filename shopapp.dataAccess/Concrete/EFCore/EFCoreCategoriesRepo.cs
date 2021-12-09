using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using shopapp.dataAccess.Abstract;
using shopapp.entity;

namespace shopapp.dataAccess.Concrete.EFCore
{
    public class EFCoreCategoriesRepo : EFCoreGenericRepo<Category, ShopContext>, ICategoryRepo
    {
        public void deleteFromCategory(int productId, int categoryId)
        {
            using (var db = new ShopContext())
            {
                var command = "delete from productcategory where ProductId=@p0 and CategoryId=@p1";
                db.Database.ExecuteSqlRaw(command, productId, categoryId);

            }
        }

        public Category getByIdWithProducts(int id)
        {
            using (var db = new ShopContext())
            {
                return db.Categories
                        .Where(i => i.CategoryId == id)
                        .Include(i => i.ProductCategories)
                        .ThenInclude(i => i.product)
                        .FirstOrDefault();
            }
        }

        public List<Category> getPopularCategory()
        {
            throw new NotImplementedException();
        }
    }
}