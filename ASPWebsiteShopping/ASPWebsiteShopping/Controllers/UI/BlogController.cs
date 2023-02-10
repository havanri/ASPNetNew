using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers.UI
{
	[Route("blog")]
	public class BlogController : Controller
	{
		public IActionResult Index()
		{
            return View("Views/UI/Blog/Index.cshtml");
        }
	}
}
