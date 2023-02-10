using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers.UI
{
	[Route("about")]
	public class AboutController : Controller
	{
		public IActionResult Index()
		{
            return View("Views/UI/About/Index.cshtml");
        }
	}
}
