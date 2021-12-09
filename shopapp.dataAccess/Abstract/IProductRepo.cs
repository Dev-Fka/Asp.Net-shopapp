using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.entity;

namespace shopapp.dataAccess.Abstract
{
    public interface IProductRepo : IRepo<Product>
    {
        Product getProductDetails(string url);
        List<Product> getSearchResult(string name);
        List<Product> getTop5Product();
        List<Product> getProductsByCategory(string name, int page, int pageSize);
        int getCountByCategory(string category);
        List<Product> getHomePageProducts();

        Product getByIdWithCategory(int id);
    }
}