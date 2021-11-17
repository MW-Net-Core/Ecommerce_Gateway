using Ecommerce_CatalogueManagmentService.Entities.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ecommerce_CatalogueManagmentService.Repository.DAL.Interfaces;
using Ecommerce_CatalogueManagmentService.Models;

namespace Ecommerce_CatalogueManagmentService.Repository.DAL.Interfaces
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationDbContext _context;
        public StatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<StatusVM> addStatus(StatusVM statusVM)
        {
            Status status = new Status
            {
                StatusId = statusVM.StatusId,
                StatusName = statusVM.StatusName,
                StatuDescription = statusVM.StatuDescription
            };
            var result = await _context.TblStatus.AddAsync(status);
            await _context.SaveChangesAsync();
            if (result != null)
            {
                return statusVM;
            }
            else
            {
               StatusVM vM = new StatusVM
               {
                   StatusId = Guid.NewGuid(),
                   StatusName = "Error",
                   StatuDescription = "Error Pro Max"
               };
                return vM;
            }
        }
        public async Task<List<StatusVM>> getall()
        {
            return await _context.TblStatus
                .Select(o => new StatusVM
                {
                    StatusId = o.StatusId,
                    StatusName = o.StatusName,
                    StatuDescription = o.StatuDescription
                    //}).Where(x => x.StatusId == Guid.NewGuid()).ToListAsync();
                }).ToListAsync();
        }
        public async Task<bool> checkStatus(StatusVM statusVM)
        {
            var result = await _context.TblStatus.Where(x => x.StatusName.Equals(statusVM.StatusName)).FirstOrDefaultAsync();
            
            if(result != null)
                return true;
            return false;
        }
        public async Task<bool> checkStatusId(StatusVM statusVM)
        {
            var result = await _context.TblStatus.Where(x => x.StatusId.Equals(statusVM.StatusId)).FirstOrDefaultAsync();
            if (result != null)
                return true;
            return false;
        }
        public async Task<StatusVM> updateStatus(StatusVM statusVM)
        {
            var status = await _context.TblStatus.FirstOrDefaultAsync(o => o.StatusId.Equals(statusVM.StatusId));
            if (status != null)
            {
                status.StatusName = statusVM.StatusName;
                status.StatuDescription = statusVM.StatuDescription;

                _context.Entry(status).State = EntityState.Modified;
                
                await _context.SaveChangesAsync();
                
                return statusVM;
            }
            else
            {
                StatusVM vM = new StatusVM
                {
                    StatusId = Guid.NewGuid(),
                    StatusName = "Error",
                    StatuDescription = "Error Pro Max"
                };
                return vM;
            }
        }
        public async Task<bool> deleteStatus(Guid? id)
        {
            var status = new Status
            {
                StatusId = (Guid)id
            };
            _context.Remove(status);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}