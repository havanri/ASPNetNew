using ASPWebsiteShopping.Models;
using ASPWebsiteShopping.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers
{
    public class SettingController : Controller
    {
        private readonly IWebHostEnvironment _whostEnvironment;
        private readonly ISettingService _settingService;
        public SettingController(
            IWebHostEnvironment whostEnvironment,
                ISettingService settingService)
        {
            _whostEnvironment = whostEnvironment;
            _settingService = settingService;
        }

        public IActionResult Index()
        {
            var model = new SettingViewModel();
            model.Settings = _settingService.GetList();
            return View("Views/Admin/Setting/Index.cshtml", model);
        }
        public IActionResult Create()
        {
            var model = new SettingViewModel();
            return View("Views/Admin/Setting/Create.cshtml", model);
        }
        [HttpPost]
        public IActionResult Create(SettingViewModel viewModel)
        {
            _settingService.Insert(viewModel);
            return RedirectToAction("Index");
        }
        
        public void Delete(int? id)
        {
            if (id == null || id == 0)
            {
                NotFound();
            }
            var setting = _settingService.GetSettingById(id);
            /*if(setting != null)
            {
                _settingService.DeleteById(setting);
            }*/
            if (setting != null)
            {
                setting.DeletedAt = DateTime.Now;
                /*_settingService.UpdateCate(setting);*/
            }
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var model = new SettingViewModel();
            model.Setting = _settingService.GetSettingById(id);
            if (model.Setting == null)
            {
                return NotFound();
            }
            return View("Views/Admin/Setting/Edit.cshtml", model);
        }
        [HttpPost]
        public IActionResult Edit(SettingViewModel viewModel)
        {
            _settingService.Update(viewModel);
            return RedirectToAction("Index");
        }
    }
}
