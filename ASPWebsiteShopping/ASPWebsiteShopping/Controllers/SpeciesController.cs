using ASPWebsiteShopping.Models;
using ASPWebsiteShopping.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers
{
	public class SpeciesController : Controller
	{
        private readonly ISpeciesService _SpeciesService;
        private readonly IAttributeService _AttributeService;

        public SpeciesController(ISpeciesService SpeciesService,IAttributeService AttributeService)
        {
            _SpeciesService = SpeciesService;
            _AttributeService = AttributeService;
        }
        public IActionResult Index(int? id)
        {
            var model = new SpeciesViewModel();
            model.SpeciesList = _SpeciesService.GetAllSpeciesListByAttributeId(id);
            var attribute = _AttributeService.GetAttributeById(id);
            model.ProductAttribute = attribute;
            ViewData["Page"] = "Thuộc tính "+attribute.Name.ToString();
            return View("Views/Admin/Species/Index.cshtml", model);
        }
        public IActionResult Create(int? id)
        {
            //
            SpeciesViewModel model = new SpeciesViewModel();
            //get attribute
            var attribute = _AttributeService.GetAttributeById(id);
            model.ProductAttribute = attribute;
            ViewData["AttributeId"] = id;
            ViewData["Page"] = "Thêm chủng loại cho thuộc tính " + attribute.Name.ToString();
            

            return View("Views/Admin/Species/Create.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SpeciesViewModel SpeciesViewModel,int attributeId)
        {
            /*            SpeciesViewModel.Speciess = null;
                        SpeciesViewModel.Species.Products = null;
                        SpeciesViewModel.Species.Slug = StringExtendsions.Slugify(SpeciesViewModel.Species.Name);*/
            Species Species = new Species()
            {
                Name = SpeciesViewModel.Species.Name,
                AttributeId= attributeId
            };
            _SpeciesService.AddSpecies(Species);
            return RedirectToAction("Index", new {id= attributeId });
            /*return View("Views/Admin/Species/Create.cshtml", SpeciesViewModel);*/
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var model = new SpeciesViewModel();
            //get Species
            Species Species = _SpeciesService.GetSpeciesById(id);
            if (Species == null)
            {
                return NotFound();
            }
            model.Species = Species;

            return View("Views/Admin/Species/Edit.cshtml", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SpeciesViewModel SpeciesViewModel)//integer : so nguyen //string:chuoi //obj
        {

            try
            {
                Species Species = _SpeciesService.GetSpeciesById(SpeciesViewModel.Species.Id);

                //Data
                Species.Name = SpeciesViewModel.Species.Name;
                _SpeciesService.UpdateSpecies(Species);
                return RedirectToAction("Index", new { id = Species.AttributeId });
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            /*            return View("Views/Admin/Species/Edit.cshtml", SpeciesViewModel);*/
        }


        [HttpGet]
        public void Delete(int? id)
        {
            if (id == null || id == 0)
            {
                NotFound();
            }
            var Species = _SpeciesService.GetSpeciesById(id);
            if (Species != null)
            {
                _SpeciesService.DeleteByObj(Species);
            }
        }
    }
}
