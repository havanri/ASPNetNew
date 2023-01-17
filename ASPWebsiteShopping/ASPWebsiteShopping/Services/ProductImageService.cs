using ASPWebsiteShopping.Data;
using ASPWebsiteShopping.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPWebsiteShopping.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly ApplicationDbContext _db;
        public ProductImageService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<ProductImage> GetAllProductImages()
        {
            return _db.ProductImages.ToList();
        }
        public ProductImage GetProductImageById(int? id)
        {
            if (id == null || id == 0)
            {
                return null;
            }
            var ProductImage = _db.ProductImages.FirstOrDefault(x => x.Id == id);
            return ProductImage;
        }
        public void DeleteByObj(ProductImage productImage)
        {
            _db.ProductImages.Remove(productImage);
            _db.SaveChanges();
        }
        public void UpdateProductImage(ProductImage productImage)
        {
            _db.ProductImages.Update(productImage);
            _db.SaveChanges();
        }
        public void AddProductImage(ProductImage productImage)
        {
            _db.ProductImages.Add(productImage);
            _db.SaveChanges();
        }

        public void DeleteRange(List<ProductImage> productImages)
        {
            _db.ProductImages.RemoveRange(productImages);
            _db.SaveChanges();
        }
    }
}
