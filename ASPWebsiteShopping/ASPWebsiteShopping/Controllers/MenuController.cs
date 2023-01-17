using ASPWebsiteShopping.Components;
using ASPWebsiteShopping.Extendsions;
using ASPWebsiteShopping.Models;
using ASPWebsiteShopping.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers
{
	public class MenuController : Controller
	{
        private readonly IMenuService _MenuService;

        public MenuController(IMenuService MenuService)
        {
            _MenuService = MenuService;
        }

        public IActionResult Index()
        {
            var model = new MenuViewModel();
            model.Menus = _MenuService.GetAllMenus();
            return View("Views/Admin/Menu/Index.cshtml", model);
        }
        public IActionResult Create()
        {


            //
            Console.WriteLine("test");
            var model = new MenuViewModel();
            IEnumerable<Menu> Menus = _MenuService.GetAllMenus();

            //convert -> htmlString[option]
            MenuRecusive recusive = new MenuRecusive(Menus);
            HtmlString htmlOption = new HtmlString(recusive.RecusiveModel(""));
            ViewData["htmlOption"] = htmlOption;

            return View("Views/Admin/Menu/Create.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MenuViewModel MenuViewModel)
        {
            Menu Menu = new Menu()
            {
                Name = MenuViewModel.Menu.Name,
                Slug = StringExtendsions.Slugify(MenuViewModel.Menu.Name),
                ParentId = MenuViewModel.Menu.ParentId,
                CreatedAt = DateTime.Now,
            };
            _MenuService.AddMenu(Menu);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var model = new MenuViewModel();
            IEnumerable<Menu> Menus = _MenuService.GetAllMenus();
            //get Menu
            Menu Menu = _MenuService.GetMenuById(id);
            if (Menu == null)
            {
                return NotFound();
            }
            model.Menu = Menu;
            //convert -> htmlString[option]
            MenuRecusive recusive = new MenuRecusive(Menus);
            HtmlString htmlOption = new HtmlString(recusive.RecusiveModel(Menu.ParentId.ToString()));
            ViewData["htmlOption"] = htmlOption;

            return View("Views/Admin/Menu/Edit.cshtml", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MenuViewModel MenuViewModel)
        {
            try
            {
                Menu Menu = _MenuService.GetMenuById(MenuViewModel.Menu.Id);

                //Data
                Menu.Id = MenuViewModel.Menu.Id;
                Menu.Name = MenuViewModel.Menu.Name;
                Menu.Slug = StringExtendsions.Slugify(MenuViewModel.Menu.Name);
                Menu.ParentId = MenuViewModel.Menu.ParentId;
                Menu.UpdatedAt = DateTime.Now;
                _MenuService.UpdateMenu(Menu);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return NotFound();
            }

        }


        [HttpGet]
        public void Delete(int? id)
        {
            if (id == null || id == 0)
            {
                NotFound();
            }
            var Menu = _MenuService.GetMenuById(id);
            if (Menu != null)
            {
                Menu.DeletedAt = DateTime.Now;
                _MenuService.UpdateMenu(Menu);
            }
        }
    }
}
