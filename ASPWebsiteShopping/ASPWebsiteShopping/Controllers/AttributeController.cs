using ASPWebsiteShopping.Components;
using ASPWebsiteShopping.Extendsions;
using ASPWebsiteShopping.Models;
using ASPWebsiteShopping.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers
{
    public class AttributeController : Controller
    {
        private readonly IAttributeService _AttributeService;

        public AttributeController(IAttributeService AttributeService)
        {
            _AttributeService = AttributeService;
        }
        [Route("ProductAttribute")]
        [Route("ProductAttribute/Index")]
        public IActionResult Index()
        {
            var model = new AttributeViewModel();
            model.ProductAttributes = _AttributeService.GetAllAttributes();
            return View("Views/Admin/Attribute/Index.cshtml", model);
        }
        public IActionResult Create()
        {
            //
            Console.WriteLine("test");
            var model = new AttributeViewModel();

            return View("Views/Admin/Attribute/Create.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AttributeViewModel AttributeViewModel)
        {
            /*            AttributeViewModel.Attributes = null;
                        AttributeViewModel.Attribute.Products = null;
                        AttributeViewModel.Attribute.Slug = StringExtendsions.Slugify(AttributeViewModel.Attribute.Name);*/
            ProductAttribute Attribute = new ProductAttribute()
            {
                Name = AttributeViewModel.ProductAttribute.Name,
            };
            _AttributeService.AddAttribute(Attribute);
            return RedirectToAction("Index");
            /*return View("Views/Admin/Attribute/Create.cshtml", AttributeViewModel);*/
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var model = new AttributeViewModel();
            IEnumerable<ProductAttribute> Attributes = _AttributeService.GetAllAttributes();
            //get Attribute
            ProductAttribute Attribute = _AttributeService.GetAttributeById(id);
            if (Attribute == null)
            {
                return NotFound();
            }
            model.ProductAttribute = Attribute;

            return View("Views/Admin/Attribute/Edit.cshtml", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AttributeViewModel AttributeViewModel)//integer : so nguyen //string:chuoi //obj
        {

            try
            {
                ProductAttribute Attribute = _AttributeService.GetAttributeById(AttributeViewModel.ProductAttribute.Id);

                //Data
                Attribute.Id = AttributeViewModel.ProductAttribute.Id;
                Attribute.Name = AttributeViewModel.ProductAttribute.Name;
                _AttributeService.UpdateAttribute(Attribute);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            /*            return View("Views/Admin/Attribute/Edit.cshtml", AttributeViewModel);*/
        }


        [HttpGet]
        public void Delete(int? id)
        {
            if (id == null || id == 0)
            {
                NotFound();
            }
            var Attribute = _AttributeService.GetAttributeById(id);
            if (Attribute != null)
            {
                _AttributeService.DeleteByObj(Attribute);
            }
        }
    }
}
