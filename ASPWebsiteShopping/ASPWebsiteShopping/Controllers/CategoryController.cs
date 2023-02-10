using ASPWebsiteShopping.Components;
using ASPWebsiteShopping.Extendsions;
using ASPWebsiteShopping.Models;
using ASPWebsiteShopping.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;


namespace ASPWebsiteShopping.Controllers
{
    [Authorize(Policy = "ProductRole")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _whostEnvironment;
        

        public CategoryController(ICategoryService categoryService,IWebHostEnvironment whostEnvironment)
        {
            _whostEnvironment = whostEnvironment;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var model = new CategoryViewModel();
            model.Categories = _categoryService.GetAllCategories();
            

            return View("Views/Admin/Category/Index.cshtml",model);
        }
        [Authorize(Policy = "CreateCategory")]
        public IActionResult Create()
        {
            //
            Console.WriteLine("test");
            var model = new CategoryViewModel();
            IEnumerable<Category> categories = _categoryService.GetAllCategories();

            //convert -> htmlString[option]
            Recusive recusive = new Recusive(categories);
            HtmlString htmlOption = new HtmlString(recusive.RecusiveModel(""));
            ViewData["htmlOption"] = htmlOption;

            return View("Views/Admin/Category/Create.cshtml",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( CategoryViewModel categoryViewModel)
        {
/*            categoryViewModel.Categories = null;
            categoryViewModel.Category.Products = null;
            categoryViewModel.Category.Slug = StringExtendsions.Slugify(categoryViewModel.Category.Name);*/
                Category category = new Category()
                {
                    Name = categoryViewModel.Category.Name,
                    Slug = StringExtendsions.Slugify(categoryViewModel.Category.Name),
                    ParentId = categoryViewModel.Category.ParentId,
                    CreatedAt = DateTime.Now,
                };
                _categoryService.AddCategory(category);
                return RedirectToAction("Index");
            /*return View("Views/Admin/Category/Create.cshtml", categoryViewModel);*/
        }
        [Authorize(Policy = "EditCategory")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var model = new CategoryViewModel();
            IEnumerable<Category> categories = _categoryService.GetAllCategories();
            //get category
            Category category = _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            model.Category = category;
            //convert -> htmlString[option]
            Recusive recusive = new Recusive(categories);
            HtmlString htmlOption = new HtmlString(recusive.RecusiveModel(category.ParentId.ToString()));
            ViewData["htmlOption"] = htmlOption;

            return View("Views/Admin/Category/Edit.cshtml", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryViewModel categoryViewModel)//integer : so nguyen //string:chuoi //obj
        {
                
                try
                {
                    Category category = _categoryService.GetCategoryById(categoryViewModel.Category.Id);

                    //Data
                    category.Id = categoryViewModel.Category.Id;
                    category.Name = categoryViewModel.Category.Name;
                    category.Slug = StringExtendsions.Slugify(categoryViewModel.Category.Name);
                    category.ParentId = categoryViewModel.Category.ParentId;
                    category.UpdatedAt = DateTime.Now;
                    _categoryService.UpdateCategory(category);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return NotFound();
                }
/*            return View("Views/Admin/Category/Edit.cshtml", categoryViewModel);*/
        }


        [HttpGet]
        [Authorize(Policy = "DeleteCategory")]
        public void Delete(int? id)
        {
            if (id == null || id == 0)
            {
                NotFound();
            }
            var category = _categoryService.GetCategoryById(id);
            if (category != null)
            {
                category.DeletedAt = DateTime.Now;
                _categoryService.UpdateCategory(category);
            }        
        }
    }
}
