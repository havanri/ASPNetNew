namespace ASPWebsiteShopping.Models
{
	public class Customer
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public string UserName { get; set; }
        public List<Order> Orders { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}
