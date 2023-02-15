using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Services
{
    public interface IProductService
    {
        void AddProduct(Product product);
        void DeleteById(Product product);
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int? id);
        Product GetProductBySlug(string? slug);
        void UpdateProduct(Product product);
        IEnumerable<Product> onlyAllProduct();
        IEnumerable<Product> GetProductsBySearch(string key_word);

    }
}