using Ecommerce_CatalogueManagmentService.Entities.DO;
using Ecommerce_CatalogueManagmentService.Repository.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_CatalogueManagmentService.Repository.DAL
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> categoryIsPresent(Guid? id)
        {
            var categoryIsAvailable = await _context.TblCategory.Where(x => x.CategoryId == id).FirstOrDefaultAsync();
            if (categoryIsAvailable != null)
                return true;
            return false;
        }
        public async Task<bool> productIsPresent(Guid? id)
        {
            var productPresent = await _context.TblProduct.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            if (productPresent != null)
                return true;
            return false;
        }

    }
}