using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_CatalogueManagmentService.Models;

namespace Ecommerce_CatalogueManagmentService.Business.BAL
{
    public interface ICategoryManager
    {
        Task<bool> CategoryExist(CategoryVM categoryVM);
        Task<ResponseVM> AddCategory(CategoryVM categoryVM);
        Task<ResponseVM> UpdateCategory(CategoryVM categoryVM);
        Task<List<CategoryVM>> GetAllCategories();
    }
}
