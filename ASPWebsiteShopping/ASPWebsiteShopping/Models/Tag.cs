
using System.ComponentModel.DataAnnotations;

namespace ASPWebsiteShopping.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public List<ProductTag> ProductTags { get; set; }
    }
}
