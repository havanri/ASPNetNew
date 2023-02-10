using ASPWebsiteShopping.Models;
using ASPWebsiteShopping.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers
{
    [Route("pro")]
    public class ProductDetailController : Controller
    {

        private readonly IProductService _productService;
        private readonly IAttributeService _attributeService;
        public ProductDetailController(IProductService productService,IAttributeService attributeService)
        {
            _productService = productService;
            _attributeService = attributeService;   
        }

        [Route("{slug?}")]
        public IActionResult Detail(string? slug)
        {
            var model = new DetailProductViewModel();
            var product  = _productService.GetProductBySlug(slug);
            model.Product= product;


            //get attribute and species of attribute's of product
            List<ProductAttribute> productAttributes = _attributeService.GetAttributeByProduct(product).ToList();
            foreach (var attribute in productAttributes.ToList())
            {
                if (attribute.ListSpecies.Count==0)
                {
                    productAttributes.Remove(attribute);
                }
            }
            model.Attributes = productAttributes;
            return View("Views/UI/ProductDetail/Index.cshtml", model);
        }

    }
}
