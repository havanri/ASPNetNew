using ASPWebsiteShopping.Data;
using ASPWebsiteShopping.Models;
using Microsoft.EntityFrameworkCore;
using SixLabors.Fonts.Tables.AdvancedTypographic;

namespace ASPWebsiteShopping.Services
{
    public class SpeciesService : ISpeciesService
    {
        private readonly ApplicationDbContext _db;
        public SpeciesService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Species> GetAllSpeciesList()
        {
            return _db.ListSpecies.Include(a => a.ProductAttribute).ToList();
        }
        public Species GetSpeciesById(int? id)
        {
            if (id == null || id == 0)
            {
                return null;
            }
            var Species = _db.ListSpecies.Include(a=>a.ProductAttribute).FirstOrDefault(x => x.Id == id);
            return Species;
        }
        public void DeleteByObj(Species Species)
        {
            _db.ListSpecies.Remove(Species);
            _db.SaveChanges();
        }
        public void UpdateSpecies(Species Species)
        {
            _db.ListSpecies.Update(Species);
            _db.SaveChanges();
        }
        public void AddSpecies(Species Species)
        {
            _db.ListSpecies.Add(Species);
            _db.SaveChanges();
        }

        public IEnumerable<Species> GetAllSpeciesListByAttributeId(int? id)
        {
            return _db.ListSpecies.Where(e=>e.AttributeId==id).ToList();
        }
        public bool checkSpeciesReturnBool(string speciesName)
        {
            var species = _db.ListSpecies.FirstOrDefault(x => x.Name == speciesName);
            if (species != null)
            {
                return true;
            }
            return false;
        }

        public void DeleteRange(List<Species> ListSpecies)
        {
            _db.ListSpecies.RemoveRange(ListSpecies);
            _db.SaveChanges();
        }
    }
}
