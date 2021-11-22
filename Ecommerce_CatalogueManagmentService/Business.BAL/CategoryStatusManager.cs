using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_CatalogueManagmentService.Business.BAL.Interface;
using Ecommerce_CatalogueManagmentService.Models;
using Ecommerce_CatalogueManagmentService.Repository.DAL.Interfaces;

namespace Ecommerce_CatalogueManagmentService.Business.BAL
{
    public class CategoryStatusManager : ICategoryStatusManager
    {
        IStatusCategoryRepository _statusCategoryRepository;
        public CategoryStatusManager(IStatusCategoryRepository statusCategoryRepository)
        {
            _statusCategoryRepository = statusCategoryRepository;
        }
        public async Task<ResponseVM> addStatusCategory(StatusCategoryVM statusCategoryVM)
        {
            Guid statusId = statusCategoryVM.StatusId;
            Guid categoryId = statusCategoryVM.CategoryId;


            bool statusExist = await _statusCategoryRepository.statusIsPresent(statusId);
            bool categoryExist = await _statusCategoryRepository.categoryIsPresent(categoryId);

            if(statusExist == true || categoryExist == true)
            {
                bool inserted = await _statusCategoryRepository.insertData(statusCategoryVM);
                if (inserted)
                    return new ResponseVM { Status = "Sucess", Message = "Inserted" };
                return new ResponseVM { Status = "Error", Message = "Not Inserted" };
            }
            return new ResponseVM { Status = "Error", Message = "Feilds are empty so cannot be Inserted" };


        }
        public async Task<ResponseVM> editStatusCategory(StatusCategoryVM statusCategoryVM)
        {
            Guid statusId = statusCategoryVM.StatusId;
            Guid categoryId = statusCategoryVM.CategoryId;
            Guid statusCategoryId = statusCategoryVM.CsId;

            bool statusExist = await _statusCategoryRepository.statusIsPresent(statusId);
            bool categoryExist = await _statusCategoryRepository.categoryIsPresent(categoryId);
            bool statuscategoryExist = await _statusCategoryRepository.statusCategoryIdIsPresent(statusCategoryId);


            if (statusExist && categoryExist && statuscategoryExist)
            {
                bool updated = await _statusCategoryRepository.updateData(statusCategoryVM);
                if (updated)
                    return new ResponseVM { Status = "Sucess", Message = "Updated" };
                return new ResponseVM { Status = "Error", Message = "Not Updated" };
            }
            return new ResponseVM { Status = "Error", Message = "Feilds are empty so cannot be Updated" };

        }
    }
}