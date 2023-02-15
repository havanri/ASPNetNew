namespace ASPWebsiteShopping.Models
{
	public class OrderViewModel
	{
		public Order Order { get; set; }
		public List<Order> Orders { get; set; }
		public List<OrderItem> OrderItems { get; set; }
		public List<Product>? Products { get; set; }	
	}
}
