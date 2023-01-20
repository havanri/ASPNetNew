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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //set Product - Category one-to-many
            modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.CategoryId);

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