using Ecommerce_CatalogueManagmentService.Business.BAL.Interface;
using Ecommerce_CatalogueManagmentService.Models;
using Ecommerce_CatalogueManagmentService.Repository.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_CatalogueManagmentService.Business.BAL
{
    public class ProductManager : IProductManager
    {
        private readonly IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ResponseVM> EditProduct(ProductVM productVM)
        {
            bool ispresnt = await _productRepository.checkProductId((Guid)productVM.ProductId);
            if (ispresnt)
            {
                await _productRepository.UpdateProduct(productVM);
                return new ResponseVM { Status = "Success", Message = "Updated Successfully" };
            }
            else
            {
                return new ResponseVM { Status = "Error", Message = "Not Updated Successfully" };
            }
        }

        public async Task<ResponseVM> InsertProduct(ProductVM productVM)
        {
            string name = productVM.ProductName;

            bool isPresnt = await _productRepository.checkProductNameExists(name);
            if (isPresnt)
            {
                return new ResponseVM { Status = "Error", Message= "Not Added successfully" };
            }
            await _productRepository.AddProduct(productVM);
            return new ResponseVM { Status = "Success", Message = "Added successfully" };

        }
    }
}
