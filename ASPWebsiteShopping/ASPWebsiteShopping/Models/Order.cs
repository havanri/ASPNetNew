using System.ComponentModel.DataAnnotations.Schema;

namespace ASPWebsiteShopping.Models
{
	public class Order
	{
		public int Id { get; set; }
		public string Status { get; set; }
		public string PaymentMethod { get; set; }
		public string Phone { get; set; }
		public string Notes { get; set; }
		public string DeliveryAddress { get; set; }
		public decimal Total { get; set; }
		public int? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public ICollection<Product> Products { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
