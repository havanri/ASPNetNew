using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers
{
    public class UIController : Controller
    {
        public IActionResult Contact()
        {
            return View("Views/UI/Contact/Index.cshtml");
        }
        public IActionResult Index()
        {
            return View("Views/UI/Home/Index.cshtml");
        }
        public IActionResult Shop()
        {
            return View("Views/UI/Products/Index.cshtml");
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
