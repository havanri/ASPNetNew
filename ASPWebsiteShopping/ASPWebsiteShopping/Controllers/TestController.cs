using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View("Views/Admin/Test/CRUDCategory.cshtml");
        }
    }
}
