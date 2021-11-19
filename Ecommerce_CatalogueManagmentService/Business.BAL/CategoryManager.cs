using Ecommerce_CatalogueManagmentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_CatalogueManagmentService.Repository.DAL.Interfaces;

namespace Ecommerce_CatalogueManagmentService.Business.BAL
{
    public class CategoryManager : ICategoryManager
    {

        private readonly ICategoryRepository _categoryRepository;
        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        // getting the values of categoey Id and name if both exists then true else false
        public async Task<bool> CategoryExist(CategoryVM categoryVM)
        {
            bool res = await _categoryRepository.CategoryExist(categoryVM);
            if (res)
                return true;
            return false;
        }


        // add category 
        public async Task<ResponseVM> AddCategory(CategoryVM categoryVM)
        {

            //category and status Checking
            bool categoryexist = await this.CategoryExist(categoryVM);
           

            if(categoryexist == false)
            {
                await _categoryRepository.AddCategory(categoryVM);
                return new ResponseVM { Status = "Success", Message = "Category Added Successfully" };
            }
            return new ResponseVM { Status = "Error", Message = "Category Already exists and status not found" };


        }
       
        //updates category
        public async Task<ResponseVM> UpdateCategory(CategoryVM categoryVM)
        {
            //category and status Checking
            bool categoryidexist = await _categoryRepository.checkCategoryId((Guid)categoryVM.CategoryId);
            

            if (categoryidexist)
            {
                await _categoryRepository.UpdateCategory(categoryVM);
                return new ResponseVM { Status = "Success", Message = "Updated" };
            }
            return new ResponseVM { Status = "Error", Message = "Not Updated" };
        }
       
        
        
        //get categories list
        public async Task<List<CategoryVM>> GetAllCategories()
        {
            return await _categoryRepository.GetAllCategories();
        }

        //delete category
        public async Task<ResponseVM> DeleteCategorys(Guid? id)
        {
            bool result = await _categoryRepository.DeleteCategory(id);
            if (result)
                return new ResponseVM { Status = "Success", Message = "Deleted Successfully" };
            return new ResponseVM { Status = "Error", Message = "Not Deleted Successfully" };
        }
    }
}
