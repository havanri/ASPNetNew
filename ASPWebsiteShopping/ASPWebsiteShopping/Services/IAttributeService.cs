using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Services
{
    public interface IAttributeService
    {
        void AddAttribute(ProductAttribute Attribute);
        void DeleteByObj(ProductAttribute Attribute);
        IEnumerable<ProductAttribute> GetAllAttributes();
        ProductAttribute GetAttributeById(int? id);
        void UpdateAttribute(ProductAttribute Attribute);
        IEnumerable<ProductAttribute> GetAttributeByProduct(Product product);
    }
}