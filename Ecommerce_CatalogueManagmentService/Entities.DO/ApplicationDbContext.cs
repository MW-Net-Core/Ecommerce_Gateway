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
        public DbSet<Category> TblCategory { get; set; }
        public DbSet<Status> TblStatus { get; set; }
    }
}
