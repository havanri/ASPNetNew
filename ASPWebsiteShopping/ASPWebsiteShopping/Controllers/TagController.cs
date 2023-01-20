using ASPWebsiteShopping.Components;
using ASPWebsiteShopping.Extendsions;
using ASPWebsiteShopping.Models;
using ASPWebsiteShopping.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebsiteShopping.Controllers
{
	public class TagController : Controller
	{
		private readonly ITagService _tagService;
		public TagController(ITagService tagService)
		{
			_tagService = tagService;
		}

        public IActionResult Index()
        {
            var model = new TagViewModel();
            model.Tags = _tagService.GetAllTags();
            return View("Views/Admin/Tag/Index.cshtml", model);
        }
        public IActionResult Create()
        {
            //
            Console.WriteLine("test");
            var model = new TagViewModel();
            IEnumerable<Tag> Tags = _tagService.GetAllTags();

            return View("Views/Admin/Tag/Create.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TagViewModel TagViewModel)
        {
            /*            TagViewModel.Tags = null;
                        TagViewModel.Tag.Products = null;
                        TagViewModel.Tag.Slug = StringExtendsions.Slugify(TagViewModel.Tag.Name);*/
            Tag Tag = new Tag()
            {
                Name = TagViewModel.Tag.Name,
            };
            _tagService.AddTag(Tag);
            return RedirectToAction("Index");
            /*return View("Views/Admin/Tag/Create.cshtml", TagViewModel);*/
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var model = new TagViewModel();
            IEnumerable<Tag> Tags = _tagService.GetAllTags();
            //get Tag
            Tag Tag = _tagService.GetTagById(id);
            if (Tag == null)
            {
                return NotFound();
            }
            model.Tag = Tag;

            return View("Views/Admin/Tag/Edit.cshtml", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TagViewModel TagViewModel)//integer : so nguyen //string:chuoi //obj
        {

            try
            {
                Tag Tag = _tagService.GetTagById(TagViewModel.Tag.Id);

                //Data
                Tag.Id = TagViewModel.Tag.Id;
                Tag.Name = TagViewModel.Tag.Name;
                _tagService.UpdateTag(Tag);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            /*            return View("Views/Admin/Tag/Edit.cshtml", TagViewModel);*/
        }


        [HttpGet]
        public void Delete(int? id)
        {
            if (id == null || id == 0)
            {
                NotFound();
            }
            var Tag = _tagService.GetTagById(id);
            if (Tag != null)
            {
                _tagService.DeleteByObj(Tag);
            }
        }
    }
}
