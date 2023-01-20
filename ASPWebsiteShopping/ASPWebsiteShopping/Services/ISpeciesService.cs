using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Services
{
	public interface ISpeciesService
	{
		void AddSpecies(Species Species);
		void DeleteByObj(Species Species);
		IEnumerable<Species> GetAllSpeciesList();

		IEnumerable<Species> GetAllSpeciesListByAttributeId(int? id);

        Species GetSpeciesById(int? id);
		void UpdateSpecies(Species Species);
        bool checkSpeciesReturnBool(string speciesName);
        void DeleteRange(List<Species> ListSpecies);
    }
}