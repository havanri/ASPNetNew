using System.ComponentModel.DataAnnotations.Schema;

namespace ASPWebsiteShopping.Models
{
    [Table("Species")]
    public class Species
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Product> ListProduct { get; set; }
        public List<ProductSpecies> ListProductSpecies { get; set; }

        public int AttributeId { get; set; }
        [ForeignKey("AttributeId")]
        public ProductAttribute ProductAttribute { get; set; }
    }
}
