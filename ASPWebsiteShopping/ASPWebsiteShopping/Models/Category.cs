using System.ComponentModel.DataAnnotations;

namespace ASPWebsiteShopping.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string Slug { get; set; }
        public virtual List<Product> Products { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
