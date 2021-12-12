using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using shopapp.dataAccess.Abstract;
using shopapp.entity;
using shopapp.entity.obj;

namespace shopapp.dataAccess.Concrete.EFCore
{
    public class EFCoreProductRepo : EFCoreGenericRepo<Product, ShopContext>, IProductRepo
    {
        public Product getByIdWithCategory(int id)
        {
            using (var db = new ShopContext())
            {
                return db.Products
                        .Where(i => i.ProductId == id)
                        .Include(i => i.ProductCategories)
                        .ThenInclude(i => i.category)
                        .FirstOrDefault();
            }
        }

        public int getCountByCategory(string category)
        {
            using (var db = new ShopContext())
            {
                var products = db.Products.AsQueryable();
                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                                .Include(i => i.ProductCategories)
                                .ThenInclude(i => i.category)
                                .Where(i => i.ProductCategories.Any(a => a.category.Name.ToLower() == category.ToLower()));
                }
                return products.Count();

            }
        }

        public List<Product> getHomePageProducts()
        {
            using (var db = new ShopContext())
            {
                return db.Products
                        .Where(i => i.ısHome)
                        .ToList();
            }
        }

        public List<Product> getPopularProduct()
        {
            using (var db = new ShopContext())
            {
                return db.Products?.ToList();
            }
        }

        public Product getProductDetails(string url)
        {
            using (var db = new ShopContext())
            {
                return db.Products
                    .Where(i => i.Url == url)
                    .Include(i => i.ProductCategories)
                    .ThenInclude(i => i.category)
                    .FirstOrDefault();
            }
        }


        public List<Product> getProductsByCategory(string name, int page, int pageSize)
        {
            using (var db = new ShopContext())
            {
                var products = db.Products.AsQueryable();
                if (!string.IsNullOrEmpty(name))
                {
                    products = products
                                .Include(i => i.ProductCategories)
                                .ThenInclude(i => i.category)
                                .Where(i => i.ProductCategories.Any(a => a.category.Name.ToLower() == name.ToLower()));
                }
                return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            }
        }

        public List<Product> getSearchResult(string name)
        {
            using (var db = new ShopContext())
            {

                if (!string.IsNullOrEmpty(name))
                {
                    return db.Products
                                    .Where(i => i.Name.ToLower().Contains(name.ToLower())).ToList();
                }
                else
                {
                    return null;
                }

            }
        }

        public List<Product> getTop5Product()
        {
            throw new NotImplementedException();
        }

        public void update(Product entity, int[] categoryId)
        {
            using (var db = new ShopContext())
            {
                var product = db.Products
                               .Include(i => i.ProductCategories)
                               .FirstOrDefault(i => i.ProductId == entity.ProductId);

                if (product != null)
                {
                    product.Name = entity.Name;
                    product.Price = entity.Price;
                    product.ImageUrl = entity.ImageUrl;
                    product.Url = entity.Url;
                    product.Description = entity.Description;

                    product.ProductCategories = categoryId.Select(catid => new ProductCategory()
                    {
                        ProductId = entity.ProductId,
                        CategoryId = catid
                    }).ToList();

                    db.SaveChanges();
                }
            }

        }
    }
}