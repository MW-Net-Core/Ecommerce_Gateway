using Ecommerce_CatalogueManagmentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_CatalogueManagmentService.Business.BAL.Interface
{
    public interface IProductStatusManager
    {
         Task<ResponseVM> addStatusProduct(StatusProductVM statusProductVM);
         Task<ResponseVM> editStatusProduct(StatusProductVM statusProductVM);
         Task<List<ProductStatusVMList>> getAllProductsWithStatus();
    }
}
