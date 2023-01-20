namespace ASPWebsiteShopping.Models
{
    public class ProductAttribute
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Species> ListSpecies { get; set; }

    }
}
