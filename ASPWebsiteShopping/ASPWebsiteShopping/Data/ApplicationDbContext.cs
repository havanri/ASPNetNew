using ASPWebsiteShopping.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace ASPWebsiteShopping.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Setting>Settings { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTag { get; set; }
        public DbSet<ProductSpecies> ProductSpecies { get; set; }
        public DbSet<Species> ListSpecies { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<Customer> Customers{ get;set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //set Category - Product one-to-many
            modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.CategoryId);

            //set Customer - Order one-to-many
            modelBuilder.Entity<Order>()
            .HasOne(p => p.Customer)
            .WithMany(b => b.Orders)
            .HasForeignKey(p => p.CustomerId);

            
            //set Product - Album Image one-to-many
            modelBuilder.Entity<ProductImage>()
            .HasOne(p => p.Product)
            .WithMany(b => b.ProductImages)
            .HasForeignKey(p => p.ProductId);

            //set Attribute - Species one-to-many
            modelBuilder.Entity<Species>()
            .HasOne(p => p.ProductAttribute)
            .WithMany(b => b.ListSpecies)
            .HasForeignKey(p => p.AttributeId);

            //set Product-Tag Many to Many
            modelBuilder.Entity<Product>()
            .HasMany(p => p.Tags)
            .WithMany(p => p.Products)
            .UsingEntity<ProductTag>(
                j => j
                    .HasOne(pt => pt.Tag)
                    .WithMany(t => t.ProductTags)
                    .HasForeignKey(pt => pt.TagId),
                j => j
                    .HasOne(pt => pt.Product)
                    .WithMany(p => p.ProductTags)
                    .HasForeignKey(pt => pt.ProductId),
                j =>
                {
                    j.Property(pt => pt.PublicationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                    j.HasKey(t => new { t.ProductId, t.TagId });
                });
            base.OnModelCreating(modelBuilder);

            //set Product-Order Many to Many
            modelBuilder.Entity<Product>()
            .HasMany(p => p.Orders)
            .WithMany(p => p.Products)
            .UsingEntity<OrderItem>(
                j => j
                    .HasOne(pt => pt.Order)
                    .WithMany(t => t.OrderItems)
                    .HasForeignKey(pt => pt.OrderId),
                j => j
                    .HasOne(pt => pt.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(pt => pt.ProductId),
                j =>
                {
                    j.Property(pt => pt.PublicationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                    j.HasKey(t => new { t.ProductId, t.OrderId });
                });
            base.OnModelCreating(modelBuilder);
            //set Product-Species Many to Many
            modelBuilder.Entity<Product>()
            .HasMany(p => p.ListSpecies)
            .WithMany(p => p.ListProduct)
            .UsingEntity<ProductSpecies>(
                j => j
                    .HasOne(pt => pt.Species)
                    .WithMany(t => t.ListProductSpecies)
                    .HasForeignKey(pt => pt.SpeciesId),
                j => j
                    .HasOne(pt => pt.Product)
                    .WithMany(p => p.ListProductSpecies)
                    .HasForeignKey(pt => pt.ProductId),
                j =>
                {
                    j.Property(pt => pt.PublicationDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                    j.HasKey(t => new { t.ProductId, t.SpeciesId });
                });
            base.OnModelCreating(modelBuilder);

        }
    }
}