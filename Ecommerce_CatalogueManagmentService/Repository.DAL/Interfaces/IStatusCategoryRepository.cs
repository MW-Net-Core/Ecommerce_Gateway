using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_CatalogueManagmentService.Models;

namespace Ecommerce_CatalogueManagmentService.Repository.DAL.Interfaces
{
    public interface IStatusCategoryRepository
    {
        Task<bool> categoryIsPresent(Guid? id);
        Task<bool> statusIsPresent(Guid? id);
        Task<bool> statusCategoryIdIsPresent(Guid? id);

        Task<bool> insertData(StatusCategoryVM statusCategoryVM);
        Task<bool> updateData(StatusCategoryVM statusCategoryVM);






    }
}
