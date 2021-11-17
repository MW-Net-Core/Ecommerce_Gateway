using Ecommerce_CatalogueManagmentService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_CatalogueManagmentService.Repository.DAL.Interfaces
{
    public interface IStatusRepository
    {
        Task<List<StatusVM>> getall();
        Task<StatusVM> addStatus(StatusVM statusVM);
        Task<StatusVM> updateStatus(StatusVM statusVM);
       
        Task<bool> checkStatus(StatusVM statusVM);
        Task<bool> checkStatusId(StatusVM statusVM);



    }
}
