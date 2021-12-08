using Ecommerce_CatalogueManagmentService.Entities.DO;
using Ecommerce_CatalogueManagmentService.Models;
using Ecommerce_CatalogueManagmentService.Repository.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Ecommerce_CatalogueManagmentService.Repository.DAL
{
    public class StatusProductRepository : IStatusProductRepository
    {
        private readonly ApplicationDbContext _context;
        public StatusProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> productIsPresent(Guid? id)
        {
            var productPresent = await _context.TblProduct.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            if (productPresent != null)
                return true;
            return false;
        }
        public async Task<bool> statusIsPresent(Guid? id)
        {
            var statusPresent = await _context.TblStatus.Where(x => x.StatusId == id).FirstOrDefaultAsync();
            if (statusPresent != null)
                return true;
            return false;
        }
        public async Task<bool> insertData(StatusProductVM statusProductVM)
        {
            ProductStatus productStatus = new ProductStatus
            {
                PsId = Guid.NewGuid(),
                ProductId = statusProductVM.ProductId,
                StatusId = statusProductVM.StatusId
            };

            await _context.TblProductStatus.AddAsync(productStatus);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> updateData(StatusProductVM statusProductVM)
        {
            var statusproduct = await _context.TblProductStatus.FirstOrDefaultAsync(x => x.PsId.Equals(statusProductVM.PsId));
            if (statusproduct != null)
            {
                statusproduct.ProductId = statusProductVM.ProductId;
                statusproduct.StatusId = statusProductVM.StatusId;
                _context.Entry(statusproduct).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }   
        public async Task<List<ProductStatusVMList>> getAllVProductStatus()
        {
            var joined_data = await(from x in _context.TblProductStatus
                                    join status in _context.TblStatus on x.StatusId equals status.StatusId
                                    join product in _context.TblProduct on x.ProductId equals product.ProductId
                                    //where e.OwnerID == user.UID
                                    select new ProductStatusVMList
                                    {
                                        PsId = x.PsId,
                                        ProductName = product.ProductName,
                                        ProductDescription = product.ProductDescription,
                                        StatusName = status.StatusName,
                                        StatusDescription = status.StatuDescription
                                    }).ToListAsync();

            return joined_data;
        }
        public async Task<bool> ProductStatusIdPresent(Guid? id)
        {
            var statusProductId = await _context.TblProductStatus.Where(x => x.PsId == id).FirstOrDefaultAsync();
            if (statusProductId != null)
                return true;
            return false;
        }
    }
}
