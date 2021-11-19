using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_CatalogueManagmentService.Models;
using Ecommerce_CatalogueManagmentService.Repository.DAL.Interfaces;
using Ecommerce_CatalogueManagmentService.Entities.DO;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_CatalogueManagmentService.Repository.DAL
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ProductVM> AddProduct(ProductVM productVM)
        {
                Product p = new Product
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = productVM.ProductName,
                    ProductDescription = productVM.ProductDescription
                };
               
                var res = await _context.TblProduct.AddAsync(p);
                await _context.SaveChangesAsync();
                return productVM;            
        }
        public async Task<bool> checkProductId(Guid? id)
        {
            var isPresentId = await _context.TblProduct.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            if (isPresentId != null)
                return true;
            return false;
        }
        public async Task<bool> checkProductNameExists(string name)
        {
            var ispresent = await _context.TblProduct.Where(x => x.ProductName == name).FirstOrDefaultAsync();
            if (ispresent != null)
                return true;
            return false;
        }
        public async Task<ProductVM> UpdateProduct(ProductVM productVM)
        {

           var product = await _context.TblProduct.Where(x => x.ProductId == productVM.ProductId).FirstOrDefaultAsync();
           if (product != null)
            {

               
                product.ProductName = productVM.ProductName;
                product.ProductDescription = productVM.ProductDescription;
                

                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return productVM;
            }
            else
            {
                ProductVM vM = new ProductVM
                {
                    ProductId = Guid.NewGuid()
                };
                return vM;
            }



        }
    }
}
