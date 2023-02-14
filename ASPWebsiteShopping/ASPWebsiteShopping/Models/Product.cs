using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPWebsiteShopping.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:n0}")]
        public decimal Price { get; set; }
        public string FeatureImagePath { get; set; }
        [NotMapped]
        public IFormFile FeatureImage { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }  
/*        public string IdUser { get; set; }*/
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [NotMapped]
        public List<IFormFile> ProductImagesRequest { get; set; }
        public virtual List<ProductImage> ProductImages { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public List<ProductTag> ProductTags { get; set; }
        public ICollection<Order> Orders { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public ICollection<Species> ListSpecies { get; set; }
        public List<ProductSpecies> ListProductSpecies { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
