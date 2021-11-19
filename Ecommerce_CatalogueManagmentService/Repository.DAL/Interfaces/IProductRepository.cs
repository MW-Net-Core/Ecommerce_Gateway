using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_CatalogueManagmentService.Models;

namespace Ecommerce_CatalogueManagmentService.Repository.DAL.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductVM> AddProduct(ProductVM productVM);
        Task<ProductVM>UpdateProduct(ProductVM productVM);
        Task<bool> checkProductNameExists(string name);
        Task<bool> checkProductId(Guid? id);
        Task<bool> DeleteProduct(Guid? id);
        Task<List<ProductVM>> GetAllProducts();



        
    }
}
