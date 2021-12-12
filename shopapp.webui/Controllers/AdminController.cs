using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using shopapp.business.Abstract;
using shopapp.entity;
using shopapp.webui.Models;

namespace shopapp.webui.Controllers
{
    public class AdminController : Controller
    {
        private IProductService productService;
        private ICategoryService categoryService;
        public AdminController(IProductService _productService, ICategoryService _categoryService)
        {
            this.productService = _productService;
            this.categoryService = _categoryService;
        }
        public IActionResult productList()
        {
            return View(new ProductListViewModel()
            {
                Products = productService.getAll()
            });
        }
        [HttpGet]
        public IActionResult createProduct()
        {

            return View();
        }

        [HttpPost]
        public IActionResult createProduct(ProductModel p)
        {
            if (ModelState.IsValid)
            {
                var entity = new Product()
                {
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Url = p.Url
                };
                if (productService.create(entity))
                {
                    var msg = new AlertMessage()
                    {
                        message = $"{p.Name} isimli ürün eklendi",
                        alertType = $"info"
                    };

                    TempData["message"] = JsonConvert.SerializeObject(msg);
                    return RedirectToAction("productList");
                }

                var msge = new AlertMessage()
                {
                    message = $"{productService.Message} isimli ürün eklendi",
                    alertType = $"danger"
                };

                TempData["message"] = JsonConvert.SerializeObject(msge);
                return View(p);
            }
            else
            {
                return View(p);
            }
        }

        [HttpGet]
        public IActionResult editProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = productService.getByIdWithCategory((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new ProductModel()
            {
                ProductId = entity.ProductId,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price,
                ImageUrl = entity.ImageUrl,
                Url = entity.Url,
                selectedCategories = entity.ProductCategories.Select(i => i.category).ToList()
            };

            ViewBag.Categories = categoryService.getAll();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> editProduct(ProductModel model, int[] categoryId, IFormFile file)
        {
            var entity = productService.getById(model.ProductId);
            if (entity == null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            entity.Description = model.Description;
            entity.Price = model.Price;
            entity.Url = model.Url;
            productService.update(entity, categoryId);

            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extension}");
                entity.ImageUrl = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            var msg = new AlertMessage()
            {
                message = $"{model.Name} isimli ürün güncellendi",
                alertType = $"success"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("productList");
        }

        [HttpPost]
        public IActionResult deleteProduct(int productId)
        {
            var entity = productService.getById(productId);
            if (entity != null)
            {

                productService.delete(entity);
            }
            var msg = new AlertMessage()
            {
                message = $"{entity.Name} isimli ürün silindi",
                alertType = $"danger"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("productList");

        }
        [HttpGet]
        public IActionResult categoryList()
        {
            var categoriesList = new CategoryListViewModel()
            {
                categories = categoryService.getAll()
            };
            return View(categoriesList);
        }

        [HttpGet]
        public IActionResult createCategory()
        {

            return View();
        }

        [HttpPost]
        public IActionResult createCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category()
                {
                    Name = model.Name,
                    Url = model.Url
                };
                categoryService.create(category);
                return RedirectToAction("categoryList");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult editCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = categoryService.getByIdWithProducts((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new CategoryModel()
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Url = entity.Url,
                products = entity.ProductCategories.Select(p => p.product).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult editCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = categoryService.getById(model.CategoryId);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Name = model.Name;
                entity.Url = model.Url;
                categoryService.update(entity);
                return RedirectToAction("categoryList");
            }
            else
            {
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult deleteCategory(int categoryId)
        {
            var entity = categoryService.getById(categoryId);
            if (entity == null)
            {
                return NotFound();
            }
            categoryService.delete(entity);
            return RedirectToAction("categoryList");
        }

        [HttpPost]
        public IActionResult deleteFromCategories(int productId, int categoryId)
        {
            categoryService.deleteFromCategory(productId, categoryId);
            return Redirect("/admin/categories/" + categoryId);
        }
    }
}