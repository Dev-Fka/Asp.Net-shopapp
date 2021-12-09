using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;
using shopapp.dataAccess.Abstract;

namespace shopapp.webui.Controllers
{
    //localhost:5000/home
    public class HomeController : Controller
    {
        private IProductService _productService;
        public HomeController(IProductService productService)
        {
            this._productService = productService;
        }
        //localhost:5000/home/index
        public IActionResult Index()
        {
            var products = _productService.getHomeProducts();
            ProductListViewModel listViewModel = new ProductListViewModel() { Products = products };
            return View(listViewModel);
        }

        public IActionResult search(string? q)
        {

            var products = _productService.getSearchResult(q);
            var productListModel = new ProductListViewModel() { Products = products };
            return View(productListModel);
        }
    }
}