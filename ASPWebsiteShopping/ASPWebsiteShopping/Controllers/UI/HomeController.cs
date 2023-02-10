using ASPWebsiteShopping.Models;
using ASPWebsiteShopping.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers.UI
{

    [Route("home")]
    [Route("/")]
    public class HomeController : Controller
	{
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ISliderService _sliderService;

        
        public HomeController(ICategoryService categoryService, IProductService productService,ISliderService sliderService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _sliderService = sliderService;
        }

        public IActionResult Index()
		{
            var model = new HomeViewModel();
            model.Categories = _categoryService.GetAllCategories().ToList();
            model.Products = _productService.GetAllProducts().ToList();
            model.Sliders = _sliderService.GetList();
            return View("Views/UI/Home/Index.cshtml", model);
        }
	}
}
