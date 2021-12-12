using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using shopapp.business.Abstract;
using shopapp.dataAccess.Abstract;
using shopapp.entity;

namespace shopapp.business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductRepo _productRepo;

        public ProductManager(IProductRepo productRepo)
        {
            this._productRepo = productRepo;
        }



        public bool create(Product entity)
        {
            if (validation(entity))
            {
                _productRepo.create(entity);
                return true;
            }
            return false;
        }

        public void delete(Product entity)
        {
            _productRepo.delete(entity);
        }

        public List<Product> getAll()
        {
            return _productRepo.getAll();
        }

        public Product getById(int id)
        {
            return _productRepo.getById(id);
        }

        public Product getByIdWithCategory(int id)
        {
            return _productRepo.getByIdWithCategory(id);
        }

        public int getCountByCategory(string category)
        {
            return _productRepo.getCountByCategory(category);
        }

        public Product getDetailWithCategories(string url)
        {
            return _productRepo.getProductDetails(url);
        }

        public List<Product> getHomeProducts()
        {
            return _productRepo.getHomePageProducts();
        }

        public List<Product> getProductsByCategory(string name, int page, int pageSize)
        {
            return _productRepo.getProductsByCategory(name, page, pageSize);
        }

        public List<Product> getSearchResult(string name)
        {
            return _productRepo.getSearchResult(name);
        }

        public void update(Product entity)
        {
            _productRepo.update(entity);
        }

        public void update(Product entity, int[] categoryId)
        {
            _productRepo.update(entity, categoryId);
        }

        public string? Message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool validation(Product entity)
        {
            var isValid = true;
            if (string.IsNullOrEmpty(entity.Name))
            {
                Message += "ürün ismi boş bırakılamaz. \n";
                isValid = false;
            }

            if (entity.Price < 0)
            {
                Message += "ürün ismi boş bırakılamaz. \n";
                isValid = false;
            }
            return isValid;
        }

    }
}