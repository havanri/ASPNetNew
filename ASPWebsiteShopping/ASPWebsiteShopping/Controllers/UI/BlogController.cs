using ASPWebsiteShopping.Models;
using ASPWebsiteShopping.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers.UI
{
	[Route("blog")]
	public class BlogController : Controller
	{
        private readonly IProductService _productService;
		private readonly ICategoryService _categoryService;
		public BlogController(IProductService productService,ICategoryService categoryService)
		{
			_productService = productService;
			_categoryService = categoryService;
		}
        public IActionResult Index()
		{
			var model = new blogViewModel();
			model.Categories = _categoryService.GetAllCategories().ToList();

			model.Products = _productService.GetAllProducts().ToList();
     
            return View("Views/UI/Blog/Index.cshtml",model);
        }
	}
}
