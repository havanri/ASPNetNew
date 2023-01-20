using ASPWebsiteShopping.Data;
using ASPWebsiteShopping.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPWebsiteShopping.Services
{
    public class AttributeService : IAttributeService
    {
        private readonly ApplicationDbContext _db;
        public AttributeService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<ProductAttribute> GetAllAttributes()
        {
            return _db.ProductAttributes.Include(e => e.ListSpecies).ToList();
        }
        public ProductAttribute GetAttributeById(int? id)
        {
            if (id == null || id == 0)
            {
                return null;
            }
            var Attribute = _db.ProductAttributes.Include(e => e.ListSpecies).FirstOrDefault(x => x.Id == id);
            return Attribute;
        }
        public void DeleteByObj(ProductAttribute Attribute)
        {
            _db.ProductAttributes.Remove(Attribute);
            _db.SaveChanges();
        }
        public void UpdateAttribute(ProductAttribute Attribute)
        {
            _db.ProductAttributes.Update(Attribute);
            _db.SaveChanges();
        }
        public void AddAttribute(ProductAttribute Attribute)
        {
            _db.ProductAttributes.Add(Attribute);
            _db.SaveChanges();
        }
    }
}
