using Microsoft.AspNetCore.Mvc;
using shopapp.business.Abstract;

namespace shopapp.webui.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private ICategoryService categoryService;

        public CategoriesViewComponent(ICategoryService _categoryService)
        {
            this.categoryService = _categoryService;
        }
        public IViewComponentResult Invoke()
        {
            if (RouteData.Values["action"].ToString() == "list")
                ViewBag.SelectedCategory = RouteData?.Values["id"]?.ToString();
            return View(categoryService.getAll());
        }
    }
}