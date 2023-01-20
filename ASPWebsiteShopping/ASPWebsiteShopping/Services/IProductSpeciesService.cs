using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Services
{
	public interface IProductSpeciesService
	{
		void AddProductSpecies(ProductSpecies ProductSpecies);
		void DeleteByObj(ProductSpecies ProductSpecies);
		void DeleteRange(List<ProductSpecies> ProductSpeciess);
	}
}