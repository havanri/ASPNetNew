using ASPWebsiteShopping.Data;
using ASPWebsiteShopping.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPWebsiteShopping.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _db;
        public CategoryService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _db.Categories.Where(e=>e.DeletedAt == null).Include(c=>c.Products).ToList();
        }
        public Category GetCategoryById(int? id)
        {
            if (id == null || id == 0)
            {
                return null;
            }
            var category = _db.Categories.Where(e => e.DeletedAt == null).FirstOrDefault(x => x.Id == id);
            return category;
        }
        public void DeleteByObj(Category category)
        {
            _db.Categories.Remove(category);
            _db.SaveChanges();
        }
        public void UpdateCategory(Category category)
        {
            _db.Categories.Update(category);
            _db.SaveChanges();
        }
        public void AddCategory(Category category)
        {
            _db.Categories.Add(category);
            _db.SaveChanges();
        }

        public Category GetCategoryBySlug(string? slug)
        {
            if (slug == null || slug == "")
            {
                return null;
            }
            var category = _db.Categories.Include(p=>p.Products).FirstOrDefault(x => x.Slug == slug);
            return category;
        }
    }
}
