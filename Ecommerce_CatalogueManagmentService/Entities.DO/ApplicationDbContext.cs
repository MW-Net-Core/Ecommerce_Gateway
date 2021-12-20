using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_CatalogueManagmentService.Entities.DO;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_CatalogueManagmentService.Entities.DO
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product_Category_Status>().HasKey(sc => new { sc.PscId });

        //    modelBuilder.Entity<Product_Category_Status>()
        //    .HasOne<Category>(sc => sc.Category)
        //    .WithMany(s => s.Product_Category_Status)
        //        .HasForeignKey(sc => sc.PscId);

        //    modelBuilder.Entity<Product_Category_Status>()
        //        .HasOne<Product>(sc => sc.Product)
        //        .WithMany(s => s.Product_Category_Status)
        //        .HasForeignKey(sc => sc.PscId);
        //}        public DbSet<Product_Category_Status> TblPCS { get; set; }

        
        public DbSet<Category> TblCategory { get; set; }
        public DbSet<Status> TblStatus { get; set; }
        public DbSet<Product> TblProduct { get; set; }
        public DbSet<CategoryStatus> TblCategoryStatus { get; set; }
        public DbSet<ProductStatus> TblProductStatus { get; set; }
        public DbSet<ProductCategory> TblProductCategory { get; set; }


    }
}
