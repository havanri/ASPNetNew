

namespace ASPWebsiteShopping.Models
{
    public class CategoryViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public Category Category { get; set; }
    }
}
