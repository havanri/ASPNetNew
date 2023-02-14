namespace ASPWebsiteShopping.Models
{
	public class CheckOutViewModel
	{
		public List<CartModelRequest> CartsModelRequest { get; set; }
		public InformationClient InformationClient { get; set; }
    }
	public class InformationClient
	{
        public string? Province { get; set; }
        public string? District { get; set; }
        public string? Ward { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
        public string Phone { get; set; }
    }
}
