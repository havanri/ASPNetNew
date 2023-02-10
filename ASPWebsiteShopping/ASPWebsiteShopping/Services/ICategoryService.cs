using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Services
{
    public interface ICategoryService 
    {
        void AddCategory(Category category);
        void DeleteByObj(Category category);
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int? id);
        void UpdateCategory(Category category);
        Category GetCategoryBySlug(string? slug);
    }
}