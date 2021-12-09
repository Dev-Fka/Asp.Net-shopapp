using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using shopapp.dataAccess.Abstract;
using shopapp.entity;

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
    }
}