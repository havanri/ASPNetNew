using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Services
{
    public interface IProductTagService
    {
        void AddProductTag(ProductTag productTag);
        void DeleteByObj(ProductTag productTag);

        void DeleteRange(List<ProductTag> productTags);

        void RemoveRedundancy(List<string> tags,int productId);
    }
}