using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity;

namespace shopapp.business.Abstract
{
    public interface IProductService
    {

        Product getById(int id);
        List<Product> getAll();
        void create(Product entity);
        void update(Product entity);
        void delete(Product entity);
        Product getDetailWithCategories(string url);
        List<Product> getProductsByCategory(string name, int page, int pageSize);
        int getCountByCategory(string category);
        List<Product> getHomeProducts();
        List<Product> getSearchResult(string name);

        Product getByIdWithCategory(int id);
    }
}