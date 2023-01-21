using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Admin()
        {
            return View("Views/UI/Contact/Index.cshtml");
        }
    }
}
