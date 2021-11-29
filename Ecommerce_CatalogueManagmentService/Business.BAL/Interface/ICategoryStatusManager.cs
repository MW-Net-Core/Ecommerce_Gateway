using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_CatalogueManagmentService.Models;

namespace Ecommerce_CatalogueManagmentService.Business.BAL.Interface
{
    public interface ICategoryStatusManager
    {
        public Task<ResponseVM> addStatusCategory(StatusCategoryVM statusCategoryVM);
        public Task<ResponseVM> editStatusCategory(StatusCategoryVM statusCategoryVM);

        public Task<List<CategoryStatusVMList>> getCategoryStatusList();
    
    }
}
