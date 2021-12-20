using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_CatalogueManagmentService.Repository.DAL.Interfaces
{
    public interface IProductCategoryRepository
    {
        Task<bool> productIsPresent(Guid? id);
        Task<bool> categoryIsPresent(Guid? id);
        
        


    }
}
