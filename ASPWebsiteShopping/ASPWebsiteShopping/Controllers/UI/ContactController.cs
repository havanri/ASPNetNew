using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers.UI
{
    [Route("contact")]
    public class ContactController : Controller
	{
		public IActionResult Index()
		{
            return View("Views/UI/Contact/Index.cshtml");
        }
	}
}
