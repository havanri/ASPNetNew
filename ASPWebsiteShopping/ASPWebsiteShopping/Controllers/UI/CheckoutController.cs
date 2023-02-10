using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers.UI
{
	[Route("checkout")]
	public class CheckoutController : Controller
	{
		public IActionResult Index()
		{
            return View("Views/UI/Checkout/Index.cshtml");
        }
	}
}
