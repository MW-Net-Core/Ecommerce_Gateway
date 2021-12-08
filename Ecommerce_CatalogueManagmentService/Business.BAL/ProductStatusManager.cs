using Ecommerce_CatalogueManagmentService.Business.BAL.Interface;
using Ecommerce_CatalogueManagmentService.Models;
using Ecommerce_CatalogueManagmentService.Repository.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_CatalogueManagmentService.Business.BAL
{
    public class ProductStatusManager : IProductStatusManager
    {
        private readonly IStatusProductRepository _statusProductRepository;
        public ProductStatusManager(IStatusProductRepository statusProductRepository)
        {
            _statusProductRepository = statusProductRepository;
        }

        public async Task<ResponseVM> addStatusProduct(StatusProductVM statusProductVM)
        {
            Guid statusId = statusProductVM.StatusId;
            Guid productId = statusProductVM.ProductId;

            bool statusExist = await _statusProductRepository.statusIsPresent(statusId);
            bool productExist = await _statusProductRepository.productIsPresent(productId);

            if (statusExist == true || productExist == true)
            {
                bool inserted = await _statusProductRepository.insertData(statusProductVM);
                if (inserted)
                    return new ResponseVM { Status = "Sucess", Message = "Inserted" };
                return new ResponseVM { Status = "Error", Message = "Not Inserted" };
            }
            return new ResponseVM { Status = "Error", Message = "Feilds are empty so cannot be Inserted" };
        }
        public async Task<ResponseVM> editStatusProduct(StatusProductVM statusProductVM)
        {
            Guid statusId = statusProductVM.StatusId;
            Guid ProductId = statusProductVM.ProductId;
            Guid statusProductId = statusProductVM.PsId;

            bool statusExist = await _statusProductRepository.statusIsPresent(statusId);
            bool productExist = await _statusProductRepository.productIsPresent(ProductId);
            bool statusproductExist = await _statusProductRepository.ProductStatusIdPresent(statusProductId);


            if (statusExist && productExist && statusproductExist)
            {
                bool updated = await _statusProductRepository.updateData(statusProductVM);
                if (updated)
                    return new ResponseVM { Status = "Sucess", Message = "Updated" };
                return new ResponseVM { Status = "Error", Message = "Not Updated" };
            }
            return new ResponseVM { Status = "Error", Message = "Feilds are empty so cannot be Updated" };
        }
        public async Task<List<ProductStatusVMList>> getAllProductsWithStatus()
        {
            return await _statusProductRepository.getAllVProductStatus();
        }

    }
}
