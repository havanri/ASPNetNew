namespace ASPWebsiteShopping.Models
{
    public class DetailProductViewModel
    {
        public Product Product { get; set; }
        public List<Product> Products { get; set; }   
        public List<ProductAttribute> Attributes { get; set; }
    }
}
