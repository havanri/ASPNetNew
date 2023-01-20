using System.ComponentModel.DataAnnotations.Schema;

namespace ASPWebsiteShopping.Models
{
    public class ProductSpecies
    {
        public DateTime PublicationDate { get; set; }


        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }


        public int SpeciesId { get; set; }
        [ForeignKey("SpeciesId ")]
        public Species Species { get; set; }
    }
}
