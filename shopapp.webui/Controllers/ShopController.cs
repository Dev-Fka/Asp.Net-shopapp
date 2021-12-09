using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.entity;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        public ShopController(IProductService productService)
        {
            this._productService = productService;
        }

        public IActionResult list(string category, int page = 1)
        {
            const int pageSize = 2;
            var productViewModel = new ProductListViewModel()
            {
                pageInfo = new PageInfo()
                {
                    totalItems = _productService.getCountByCategory(category),
                    currentPage = page,
                    ıtemsPerPage = pageSize,
                    currentCategory = category
                },
                Products = _productService.getProductsByCategory(category, page, pageSize)
            };
            return View(productViewModel);
        }

        public IActionResult details(string Url)
        {
            if (Url == null)
            {
                return NotFound();
            }
            Product product = _productService.getDetailWithCategories(Url);
            if (product != null)
            {
                return View(new ProductDetailModel() { Product = product, Categories = product.ProductCategories.Select(i => i.category).ToList() });
            }
            else
            {
                return NotFound();
            }
        }
    }
}