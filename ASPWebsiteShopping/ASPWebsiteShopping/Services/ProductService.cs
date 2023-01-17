﻿using ASPWebsiteShopping.Data;
using ASPWebsiteShopping.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPWebsiteShopping.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;
        public ProductService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _db.Products.Include(i=>i.ProductImages).Include(c=>c.Category).Include(t=>t.Tags).ToList();
        }
        public Product GetProductById(int? id)
        {
            if (id == null || id == 0)
            {
                return null;
            }
            var product = _db.Products.Include(t=>t.Tags).Include(i=>i.ProductImages).FirstOrDefault(x => x.Id == id);
            return product;
        }
        public void DeleteById(Product product)
        {
            _db.Products.Remove(product);
            _db.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
        }
        public void AddProduct(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
        }
    }
}