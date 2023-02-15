using ASPWebsiteShopping.Models;
using ASPWebsiteShopping.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers
{
    public class UIController : Controller
    {
        private readonly ICategoryService _categoryService ;
        private readonly IProductService _productService ;

        public UIController(
            ICategoryService categoryService ,
            IProductService productService
            )
        {
            _categoryService = categoryService;
            _productService = productService;
          

        }
        public IActionResult Index()
        {
            var model = new UIHomeViewsModel();
            model.Categories = _categoryService.GetAllCategories().ToList();
            model.products = _productService.GetAllProducts().ToList();
            return View("Views/UI/Home/Index.cshtml",model);
        }
        public IActionResult Shop()
        {
            return View("Views/UI/Products/Index.cshtml");
        }
        public IActionResult blog()
        {
            return View("Views/UI/Blog/Index.cshtml");
        }
        public IActionResult ProductDetail()
        {
            return View("Views/UI/ProductDetail/Index.cshtml");
        }
        public IActionResult Checkout()
        {
            return View("Views/UI/Checkout/Index.cshtml");
        }
    }
}
