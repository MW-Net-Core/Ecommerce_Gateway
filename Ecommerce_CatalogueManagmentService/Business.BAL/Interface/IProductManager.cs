using Ecommerce_CatalogueManagmentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_CatalogueManagmentService.Business.BAL.Interface
{
    public interface IProductManager
    {
        Task<ResponseVM> InsertProduct(ProductVM productVM);

        Task<ResponseVM> EditProduct(ProductVM productVM);

    }
}
