using ASPWebsiteShopping.Data;
using ASPWebsiteShopping.Models;

namespace ASPWebsiteShopping.Services
{
    public class ProductSpeciesService : IProductSpeciesService
    {
        private readonly ApplicationDbContext _db;
        /*private readonly UserManager<IdentityUser> _userManager;//truy van user*/
        public ProductSpeciesService(ApplicationDbContext db/*, UserManager<IdentityUser> um*/)
        {
            _db = db;
            /* _userManager = um;*/
        }
        public void AddProductSpecies(ProductSpecies ProductSpecies)
        {
            _db.ProductSpecies.Add(ProductSpecies);
            _db.SaveChanges();
        }

        public void DeleteByObj(ProductSpecies ProductSpecies)
        {
            _db.ProductSpecies.Remove(ProductSpecies);
            _db.SaveChanges();
        }

        public void DeleteRange(List<ProductSpecies> ProductSpeciess)
        {
            _db.ProductSpecies.RemoveRange(ProductSpeciess);
            _db.SaveChanges();
        }
    }
}
