using Ecommerce_CatalogueManagmentService.Repository.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_CatalogueManagmentService.Entities.DO;
using Ecommerce_CatalogueManagmentService.Models;

namespace Ecommerce_CatalogueManagmentService.Repository.DAL
{
    public class StatusCategoryRepository : IStatusCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public StatusCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> categoryIsPresent(Guid? id)
        {
            var categoryPresent = await _context.TblCategory.Where(x => x.CategoryId == id).FirstOrDefaultAsync();
            if (categoryPresent != null)
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
        public async Task<bool> insertData(StatusCategoryVM statusCategoryVM)
        {
            CategoryStatus categoryStatus = new CategoryStatus
            {
                CsId = Guid.NewGuid(),
                CategoryId = statusCategoryVM.CategoryId,
                StatusId = statusCategoryVM.StatusId
            };

            await _context.TblCategoryStatus.AddAsync(categoryStatus);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> updateData(StatusCategoryVM statusCategoryVM)
        {
            var statuscategory = await _context.TblCategoryStatus.FirstOrDefaultAsync(x => x.CsId.Equals(statusCategoryVM.CsId));
            if (statuscategory != null)
            {
                statuscategory.CategoryId = statusCategoryVM.CategoryId;
                statuscategory.StatusId = statusCategoryVM.StatusId;

                _context.Entry(statuscategory).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> statusCategoryIdIsPresent(Guid? id)
        {
            var statusCategoryId = await _context.TblCategoryStatus.Where(x => x.CsId == id).FirstOrDefaultAsync();
            if (statusCategoryId != null)
                return true;
            return false;
        }
    }
} 
