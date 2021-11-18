using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_CatalogueManagmentService.Models;

namespace Ecommerce_CatalogueManagmentService.Repository.DAL.Interfaces
{
    public interface ICategoryRepository
    {
        // category exist
        Task<bool> CategoryExist(CategoryVM categoryVM);

        // add category with status
        Task<CategoryVM> AddCategory(CategoryVM categoryVM);


        //check staus id exist
        Task<bool> checkStatusId(Guid? id);

        //check category id exist
        Task<bool> checkCategoryId(Guid? id);

        //update category
        Task<CategoryVM> UpdateCategory(CategoryVM categoryVM);


        //get the list of categories
        Task<List<CategoryVM>> GetAllCategories();
    }
}