namespace ASPWebsiteShopping.Models
{
	public class SpeciesViewModel
	{
		public Species Species { get; set; }
		public IEnumerable<Species> SpeciesList { get; set; }

		public ProductAttribute ProductAttribute { get; set; }
	}
}
