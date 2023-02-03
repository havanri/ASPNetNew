using ASPWebsiteShopping.Components;
using ASPWebsiteShopping.Extendsions;
using ASPWebsiteShopping.Models;
using ASPWebsiteShopping.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.InteropServices;
using System.Web.WebPages;


namespace ASPWebsiteShopping.Controllers
{
    [Authorize(Policy = "ProductRole")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductImageService _productImageService;
        private readonly ITagService _tagService;
        private readonly IProductTagService _productTagService;
        private readonly IAttributeService _attributeService;
        private readonly ISpeciesService _speciesService;
        private readonly IProductSpeciesService _productSpeciesService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        

        public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment, IProductImageService productImageService,ITagService tagService,IProductTagService productTagService,IAttributeService attributeService,ISpeciesService speciesService,IProductSpeciesService productSpeciesService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
            _productImageService = productImageService;
            _tagService = tagService;
            _productTagService = productTagService;
            _attributeService=attributeService;
            _speciesService=speciesService;
            _productSpeciesService=productSpeciesService;
        }

        public IActionResult Index()
        {
            ProductViewModel mymodel = new ProductViewModel();
            mymodel.Products = _productService.GetAllProducts();
            return View("Views/Admin/Product/Index.cshtml", mymodel);
        }
       
        [HttpGet]
        [Authorize(Policy = "CreateProduct")]
        public IActionResult Create()
        {
            ProductViewModel model = new ProductViewModel();
            //  var model = new ProductViewModel();
            IEnumerable<Category> categories = _categoryService.GetAllCategories();

            IEnumerable<ProductAttribute> productAttributes = _attributeService.GetAllAttributes();
            model.Attributes = productAttributes;

            //convert -> htmlString[option]
            Recusive recusive = new Recusive(categories);
            HtmlString htmlOption = new HtmlString(recusive.RecusiveModel(""));
            ViewData["htmlOption"] = htmlOption;

            return View("Views/Admin/Product/Create.cshtml", model);
        }
        [HttpPost]
        public IActionResult Create(ProductViewModel model,List<string>tags,string[] attribute_values)
        {
            Console.WriteLine("tags" + tags.Count());
            Console.WriteLine("images" + model.Product.ProductImagesRequest.Count());
            try
            {
                if (model.Product.FeatureImage != null) { 
                    //        {
                                var filename = Guid.NewGuid().ToString() + ".png";
                                var path = Path.Combine(_webHostEnvironment.WebRootPath, "image", filename);
                                var stream = System.IO.File.Create(path);
                                model.Product.FeatureImage.CopyTo(stream);
                                stream.Close();
                                model.Product.FeatureImagePath = Path.Combine("image", filename);
                                Console.WriteLine("path-------------" + model.Product.FeatureImage);
                    }
                //Create product
              var product = new Product()
              {
                 Name = model.Product.Name,
                 Price = model.Product.Price,
                 Description = model.Product.Description,
                 CategoryId = model.Product.CategoryId,
                 FeatureImagePath = model.Product.FeatureImagePath,
                 Slug = StringExtendsions.Slugify(model.Product.Name),
                 CreatedAt = DateTime.Now,
             };
             _productService.AddProduct(product);
             //create album image
             foreach(var item in model.Product.ProductImagesRequest)
                {
                    var productImage = new ProductImage();

                    var filename = Guid.NewGuid().ToString() + ".png";
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "image", filename);
                    var stream = System.IO.File.Create(path);
                    item.CopyTo(stream);
                    stream.Close();
                    productImage.ImagePath = Path.Combine("image", filename);
                    productImage.ProductId = product.Id;
                    _productImageService.AddProductImage(productImage);
                }

                //create tag
                foreach (var item in tags)
                {
                    var tag = new Tag()
                    {
                        Name = item
                    };
                    _tagService.AddTag(tag);

                    var productTag = new ProductTag()
                    {
                        TagId = tag.Id,
                        ProductId = product.Id,
                    };
                    _productTagService.AddProductTag(productTag);
                }
                //create species-product
                foreach(var itemSpecies in attribute_values)
                {
                    var species = _speciesService.GetSpeciesById(int.Parse(itemSpecies));

                    var productSpecies = new ProductSpecies()
                    {
                        SpeciesId = species.Id,
                        ProductId= product.Id,
                    };
                    _productSpeciesService.AddProductSpecies(productSpecies);
                    
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine("Loi"+e.Message);
                return null;
            }
            
        }
        [HttpGet]
        [Authorize(Policy = "EditProduct")]
        public IActionResult Edit(int? id)
        {
            ProductViewModel model = new ProductViewModel();
            if (id == null)
            {
                return NotFound();
            }
            
            Product product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            model.Product = product;
            IEnumerable<Category> categories = _categoryService.GetAllCategories();
            //convert -> htmlString[option] category
            Recusive recusive = new Recusive(categories);
            HtmlString htmlOption = new HtmlString(recusive.RecusiveModel(product.CategoryId.ToString()));
            ViewData["htmlOption"] = htmlOption;

            //----species option of attribute-------
            //--Danh sách các thuộc tính nhưng mà phải disabled các thuộc tính đã thiết lập
            var attributeList = _attributeService.GetAllAttributes();
            //
            var attributeListOfProduct = new List<ProductAttribute>();
            foreach (var speciesItem in product.ListSpecies)
            {
                foreach (var attributeItem in attributeList)
                {
                    bool containsItem = attributeListOfProduct.Any(item => item.Name == attributeItem.Name);
                    if (speciesItem.ProductAttribute.Name.Equals(attributeItem.Name) && containsItem == false)
                    {
                        attributeListOfProduct.Add(attributeItem);
                    }
                }
            }
            //return optionHtml
            OptionAttributeEdit oa = new OptionAttributeEdit(attributeList.ToList(),attributeListOfProduct);
            HtmlString htmlAttributesOption = new HtmlString(oa.ReturnOptionAttribute());
            ViewData["htmlAttributesOption"] = htmlAttributesOption;

            //--Tiếp theo show option species của thuộc tính mà đã thiết lập 
            OptionSpeciesEdit os = new OptionSpeciesEdit(attributeListOfProduct,product.ListSpecies.ToList());
            HtmlString htmlSpeciesOption = new HtmlString(os.ReturnSpeciesOptionHtml());
            ViewData["htmlSpeciesOption"] = htmlSpeciesOption;

            return View("Views/Admin/Product/Edit.cshtml", model);
        }
        [HttpPost]
        public IActionResult Edit(ProductViewModel model, List<string> tags, string[] attribute_values)
        {


            /*Console.WriteLine("tags" + tags.Count());
            Console.WriteLine("images" + model.ProductImagesRequest.Count());*/
            try
            {
                var product = _productService.GetProductById(model.Product.Id);
                if (model.Product.FeatureImage != null)
                {
                    //        {
                    var filename = Guid.NewGuid().ToString() + ".png";
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "image", filename);
                    var stream = System.IO.File.Create(path);
                    model.Product.FeatureImage.CopyTo(stream);
                    stream.Close();
                    model.Product.FeatureImagePath = Path.Combine("image", filename);
                    Console.WriteLine("path-------------" + model.Product.FeatureImage);
                    product.FeatureImagePath = model.Product.FeatureImagePath;
                }
                //transmit data product
                product.Name = model.Product.Name;
                product.Price = model.Product.Price;
                product.Description = model.Product.Description;
                product.CategoryId = model.Product.CategoryId;
                product.Slug = StringExtendsions.Slugify(model.Product.Name);
                product.UpdatedAt = DateTime.Now;

                //
                _productService.UpdateProduct(product);
               
                //create album image
                if(model.Product.ProductImagesRequest != null)
                {
                    //remove old
                    if (product.ProductImages != null)
                    {
                        _productImageService.DeleteRange(product.ProductImages.ToList());
                        /*foreach (var itemRemove in product.ProductImages)
                        {
                            _productImageService.DeleteByObj(itemRemove);
                        }*/
                    }
                    //add new
                    foreach (var item in model.Product.ProductImagesRequest)
                    {
                        var productImage = new ProductImage();
                        var filename = Guid.NewGuid().ToString() + ".png";
                        var path = Path.Combine(_webHostEnvironment.WebRootPath, "image", filename);
                        var stream = System.IO.File.Create(path);
                        item.CopyTo(stream);
                        stream.Close();
                        productImage.ImagePath = Path.Combine("image", filename);
                        productImage.ProductId = product.Id;
                        _productImageService.AddProductImage(productImage);
                    }
                }
                //create tag
                if (tags != null)
                {
                    //bẻ gãy các liên kết không tồn tại trong phiên (product_tag)
                    List<ProductTag> listRemoveLinked = new List<ProductTag>();
                    foreach (var itemRemove in product.ProductTags)
                    {
                        if(itemRemove.ProductId==product.Id && !tags.Contains(itemRemove.Tag.Name)){
                            listRemoveLinked.Add(itemRemove);
                        }
                        /*if (tags.Contains(itemRemove.Name)
                        {
                            _productTagService.DeleteByObj(itemRemove);
                        }*/

                    }
                    if(listRemoveLinked != null)
                    {
                        _productTagService.DeleteRange(listRemoveLinked);
                    }
                    //add new tag
                    foreach (var item in tags)
                    {
                        bool check = _tagService.checkTagReturnBool(item);
                        //check duplicate
                        if (check == false)//not duplicate | 
                        {

                            var tag = new Tag()
                            {
                                Name = item
                            };
                            _tagService.AddTag(tag);

                            //relationship m-m middleware product_tag
                            var productTag = new ProductTag()
                            {
                                TagId = tag.Id,
                                ProductId = product.Id,
                            };
                            _productTagService.AddProductTag(productTag);
                        }
                        //duplicate but tag of product not contain tagCheck
                        else if (check == true)
                        {
                            bool containsItem = product.Tags.Any(e => e.Name == item);
                            if(containsItem == false)
                            {
                                var tagCheck = _tagService.getTagByName(item);
                                var productTag = new ProductTag()
                                {
                                    TagId = tagCheck.Id,
                                    ProductId = product.Id,
                                };
                                _productTagService.AddProductTag(productTag);
                            }
                            
                            //relationship m-m middleware product_tag     
                        }
                        //duplicate không được bẻ gãy các liên kết 
                    }
                }
                //Species - Product
                if (attribute_values != null)
                {
                    //bẻ gãy các liên kết không tồn tại trong phiên (product_species)
                    List<ProductSpecies> listSpeciesRemoveLinked = new List<ProductSpecies>();
                    foreach (var itemRemove in product.ListProductSpecies)
                    {
                        if (itemRemove.ProductId == product.Id && !attribute_values.Contains(itemRemove.Species.Name))
                        {
                            listSpeciesRemoveLinked.Add(itemRemove);
                        }
                    }
                    if (listSpeciesRemoveLinked != null)
                    {
                        _productSpeciesService.DeleteRange(listSpeciesRemoveLinked);
                    }
                    //
                    foreach (var item in attribute_values)
                    {
                        //kiểm tra xem sản phẩm này đã tồn tại chủng loại này chưa
                        bool containsItem = product.ListSpecies.Any(e => e.Id == int.Parse(item));
                        if (containsItem == false)//chưa
                        {
                            var productSpecies = new ProductSpecies()
                            {
                                SpeciesId = int.Parse(item),
                                ProductId = product.Id,
                            };
                            _productSpeciesService.AddProductSpecies(productSpecies);
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine("Loi" + e.Message);
                return null;
            }
        }
        [Authorize(Policy = "DeleteProduct")]
        public void Delete(int? id)
        {
            var product = _productService.GetProductById(id);
            if (product != null)
            {
                _productService.DeleteById(product);
            }
        }
        public string OptionSpecies(int? id)
        {
            var htmlOption = "";
            var attribute = _attributeService.GetAttributeById(id);
            var SpeciesListByAttributeId = attribute.ListSpecies;

            //
            GetOptionByAttribute option = new GetOptionByAttribute(id, SpeciesListByAttributeId);
            htmlOption = option.ReturnHtmlOption(null);
            return htmlOption;
        }
    }
}
