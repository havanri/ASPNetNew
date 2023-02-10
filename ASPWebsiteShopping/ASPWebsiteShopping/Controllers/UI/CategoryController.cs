using ASPWebsiteShopping.Models;
using ASPWebsiteShopping.Services;
using Microsoft.AspNetCore.Mvc;
using System.Web.WebPages;

namespace ASPWebsiteShopping.Controllers.UI
{
    [Route("cat")]
    public class CategoryController : Controller
	{
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public CategoryController(ICategoryService categoryService,IProductService productService)
        {
            _categoryService = categoryService;
            _productService=productService;
        }
        [Route("{slug?}")]
        public IActionResult Detail(string? slug)
		{
            var model = new UICategoryViewModel();
            if (slug == null || slug.IsEmpty())
            {
                model.Products = _productService.GetAllProducts().ToList();
            }
            model.Category = _categoryService.GetCategoryBySlug(slug);
            model.Products = model.Category.Products;
            return View("Views/UI/Products/Index.cshtml",model);
		}
	}
}
