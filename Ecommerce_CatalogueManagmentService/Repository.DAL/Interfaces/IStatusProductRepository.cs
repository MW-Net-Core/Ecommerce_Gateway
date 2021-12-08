using Ecommerce_CatalogueManagmentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_CatalogueManagmentService.Repository.DAL.Interfaces
{
    public interface IStatusProductRepository
    {
        Task<bool> productIsPresent(Guid? id);
        Task<bool> statusIsPresent(Guid? id);
        Task<bool> insertData(StatusProductVM statusProductVM);
        Task<bool> updateData(StatusProductVM statusProductVM);
        Task<List<ProductStatusVMList>> getAllVProductStatus();
        Task<bool> ProductStatusIdPresent(Guid? id);
    }
}