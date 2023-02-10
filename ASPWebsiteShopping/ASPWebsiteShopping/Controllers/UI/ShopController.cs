using ASPWebsiteShopping.Models;
using ASPWebsiteShopping.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers.UI
{
	
	[Route("shop")]
	public class ShopController : Controller
	{
        private readonly IProductService _productService;
		public ShopController(IProductService productService)
		{
			_productService = productService;
		}
        public IActionResult Index()
		{
			var model = new UICategoryViewModel();
			model.Products = _productService.GetAllProducts().ToList();
            return View("Views/UI/Products/Index.cshtml",model);
        }
	}
}
