using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View("Views/Admin/Dashboard/Index.cshtml");
        }
    }
}
