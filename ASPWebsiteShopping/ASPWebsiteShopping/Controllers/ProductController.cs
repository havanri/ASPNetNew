using ASPWebsiteShopping.Components;
using ASPWebsiteShopping.Extendsions;
using ASPWebsiteShopping.Models;
using ASPWebsiteShopping.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Linq.Expressions;
using System.Net;


namespace ASPWebsiteShopping.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductImageService _productImageService;
        private readonly ITagService _tagService;
        private readonly IProductTagService _productTagService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        

        public ProductController(IProductService productService, ICategoryService categoryService, IWebHostEnvironment webHostEnvironment, IProductImageService productImageService,ITagService tagService,IProductTagService productTagService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
            _productImageService = productImageService;
            _tagService = tagService;
            _productTagService = productTagService;
        }

        public IActionResult Index()
        {
            ProductViewModel mymodel = new ProductViewModel();
            mymodel.Products = _productService.GetAllProducts();
            return View("Views/Admin/Product/Index.cshtml", mymodel);
        }
        [HttpGet]
        public IActionResult Create()
        {
          //  var model = new ProductViewModel();
            IEnumerable<Category> categories = _categoryService.GetAllCategories();

            //convert -> htmlString[option]
            Recusive recusive = new Recusive(categories);
            HtmlString htmlOption = new HtmlString(recusive.RecusiveModel(""));
            ViewData["htmlOption"] = htmlOption;

            return View("Views/Admin/Product/Create.cshtml");
        }
        [HttpPost]
        public IActionResult Create(Product model,List<string>tags)
        {
            Console.WriteLine("tags" + tags.Count());
            Console.WriteLine("images" + model.ProductImagesRequest.Count());
            try
            {
                if (model.FeatureImage != null) { 
                    //        {
                                var filename = Guid.NewGuid().ToString() + ".png";
                                var path = Path.Combine(_webHostEnvironment.WebRootPath, "image", filename);
                                var stream = System.IO.File.Create(path);
                                model.FeatureImage.CopyTo(stream);
                                stream.Close();
                                model.FeatureImagePath = Path.Combine("image", filename);
                                Console.WriteLine("path-------------" + model.FeatureImage);
                    }
                //Create product
              var product = new Product()
              {
                 Name = model.Name,
                 Price = model.Price,
                 Description = model.Description,
                 CategoryId = model.CategoryId,
                 FeatureImagePath = model.FeatureImagePath,
                 Slug = StringExtendsions.Slugify(model.Name),
                 CreatedAt = DateTime.Now,
             };
             _productService.AddProduct(product);
             //create album image
             foreach(var item in model.ProductImagesRequest)
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
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine("Loi"+e.Message);
                return null;
            }
            
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            Product product = _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }
            IEnumerable<Category> categories = _categoryService.GetAllCategories();
            //convert -> htmlString[option]
            Recusive recusive = new Recusive(categories);
            HtmlString htmlOption = new HtmlString(recusive.RecusiveModel(product.CategoryId.ToString()));
            ViewData["htmlOption"] = htmlOption;

            return View("Views/Admin/Product/Edit.cshtml",product);
        }
        [HttpPost]
        public IActionResult Edit(Product model, List<string> tags)
        {


            /*Console.WriteLine("tags" + tags.Count());
            Console.WriteLine("images" + model.ProductImagesRequest.Count());*/
            try
            {
                var product = _productService.GetProductById(model.Id);
                if (model.FeatureImage != null)
                {
                    //        {
                    var filename = Guid.NewGuid().ToString() + ".png";
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "image", filename);
                    var stream = System.IO.File.Create(path);
                    model.FeatureImage.CopyTo(stream);
                    stream.Close();
                    model.FeatureImagePath = Path.Combine("image", filename);
                    Console.WriteLine("path-------------" + model.FeatureImage);
                    product.FeatureImagePath = model.FeatureImagePath;
                }
                //transmit data product
                product.Name = model.Name;
                product.Price = model.Price;
                product.Description = model.Description;
                product.CategoryId = model.CategoryId;
                product.Slug = StringExtendsions.Slugify(model.Name);
                product.UpdatedAt = DateTime.Now;

                //
                _productService.UpdateProduct(product);
               
                //create album image
                if(model.ProductImagesRequest != null)
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
                    foreach (var item in model.ProductImagesRequest)
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
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Console.WriteLine("Loi" + e.Message);
                return null;
            }
        }
        public void Delete(int? id)
        {
            var product = _productService.GetProductById(id);
            if (product != null)
            {
                _productService.DeleteById(product);
            }
        }
    }
}
