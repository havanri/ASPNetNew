using ASPWebsiteShopping.Data;
using ASPWebsiteShopping.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ASPWebsiteShopping.Services
{
    public class ProductTagService : IProductTagService
    {
        private readonly ApplicationDbContext _db;
        /*private readonly UserManager<IdentityUser> _userManager;//truy van user*/
        public ProductTagService(ApplicationDbContext db/*, UserManager<IdentityUser> um*/)
        {
            _db = db;
           /* _userManager = um;*/
        }
        public void AddProductTag(ProductTag productTag)
        {
            _db.ProductTag.Add(productTag);
            _db.SaveChanges();
        }

        public void DeleteByObj(ProductTag productTag)
        {
            _db.ProductTag.Remove(productTag);
            _db.SaveChanges();
        }

        public void DeleteRange(List<ProductTag> productTags)
        {
            _db.ProductTag.RemoveRange(productTags);
            _db.SaveChanges();
        }

        public void RemoveRedundancy(List<string> tags, int productId)
        {
            /*List<ProductTag> = null;*/
            foreach(var tagItemName in tags)
            {

            }
           /* var productTags =SelectList(_db.ProductTag.Where())*/
        }
    }
}
