using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Services
{
    public interface IProductImageService
    {
        void AddProductImage(ProductImage productImage);
        void DeleteByObj(ProductImage productImage);

        void DeleteRange(List<ProductImage> productImages);
        IEnumerable<ProductImage> GetAllProductImages();
        ProductImage GetProductImageById(int? id);
        void UpdateProductImage(ProductImage productImage);
    }
}