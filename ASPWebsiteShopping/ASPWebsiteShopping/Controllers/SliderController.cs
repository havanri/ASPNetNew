using ASPWebsiteShopping.Models;
using ASPWebsiteShopping.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace ASPWebsiteShopping.Controllers
{
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IWebHostEnvironment _whostEnvironment;
        public SliderController(
            ISliderService sliderService,
            IWebHostEnvironment hostEnvironment

            )
        {
            _sliderService = sliderService; 
            _whostEnvironment = hostEnvironment;

        }
        public IActionResult Index()
        {
            var model = new SliderViewModel();
            model.Sliders = _sliderService.GetList();
            return View("Views/Admin/Slider/Index.cshtml", model);

            
        }
        [HttpGet]
        public void Delete(int? id)
        {
            var slider = _sliderService.GetSliderById(id);
            if(slider != null)
            {
                _sliderService.DeleteById(slider);
            }

           
        }
        [HttpGet]
        public IActionResult Create()
        {
            var model = new SliderViewModel();
            return View("Views/Admin/Slider/Create.cshtml", model);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var model = new SliderViewModel();
            model.Slider= _sliderService.GetSliderById(id);
            return View("Views/Admin/Slider/Edit.cshtml", model);
        }
        [HttpPost]
        public IActionResult Edit(SliderViewModel viewModel)
        {
            if (viewModel.Slider.Image!=null)
            {

                var filename = Guid.NewGuid().ToString() + ".png";
                var path = Path.Combine(_whostEnvironment.WebRootPath, "image", filename
                    );

                var stream = System.IO.File.Create(path);
                viewModel.Slider.Image.CopyTo(stream);
                stream.Close();
                viewModel.Slider.ImagePath = Path.Combine("image", filename);
            }
           
            _sliderService.Update(viewModel);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Create (SliderViewModel viewModel)
        {
            if (viewModel.Slider.Image != null)
            {

                var filename = Guid.NewGuid().ToString() + ".png";
                var path = Path.Combine(_whostEnvironment.WebRootPath, "image", filename
                    );

                var stream = System.IO.File.Create(path);
                viewModel.Slider.Image.CopyTo(stream);
                stream.Close();
                viewModel.Slider.ImagePath = Path.Combine("image", filename);

            }
            _sliderService.Insert(viewModel);
            return RedirectToAction("Index");
        }
    }
}
