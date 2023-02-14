using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers.UI
{
	[Route("cart")]
	public class CartController : Controller
	{
		public IActionResult Index()
		{
            return View("Views/UI/Checkout/Index.cshtml");
        }
	}
}
