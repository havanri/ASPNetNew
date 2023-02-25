using NuGet.DependencyResolver;

namespace ASPWebsiteShopping.Models

{
    public class ProductViewModel
    {
        public IEnumerable<Product>? Products { get; set; }

        public Product Product { get; set; }

        public IEnumerable<Category>? Categories { get; set; }

        public IEnumerable<ProductImage>? ProductImages { get; set; }

        public IEnumerable<Tag>? Tags { get; set; }

        public IEnumerable<ProductAttribute>? Attributes { get; set; }
    }
}
