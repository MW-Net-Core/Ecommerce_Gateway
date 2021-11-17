using Ecommerce_CatalogueManagmentService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_CatalogueManagmentService.Business.BAL.Interface
{
    public interface IStatusManager
    {
        Task<List<StatusVM>> GetAllStatus();
        Task<bool> checkStatusAlreadyExists(StatusVM statusVM);
        Task<bool> checkbyId(StatusVM statusVM);
        Task<ResponseVM> AddStatus(StatusVM statusVM);
        Task<ResponseVM> UpdateStatus(StatusVM statusVM);

    }
}

