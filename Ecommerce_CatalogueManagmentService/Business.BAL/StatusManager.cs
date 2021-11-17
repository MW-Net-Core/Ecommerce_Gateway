using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce_CatalogueManagmentService.Business.BAL.Interface;
using Ecommerce_CatalogueManagmentService.Models;
using Ecommerce_CatalogueManagmentService.Repository.DAL.Interfaces;

namespace Ecommerce_CatalogueManagmentService.Business.BAL
{
    public class StatusManager : IStatusManager
    {
        private readonly IStatusRepository _statusReporsitory;
        public StatusManager(IStatusRepository statusReporsitory)
        {
            _statusReporsitory = statusReporsitory;
        }
        public async Task<ResponseVM> AddStatus(StatusVM statusVM)
        {
            bool alreadyExist = await this.checkStatusAlreadyExists(statusVM);

            if (alreadyExist)
            {
                return new ResponseVM { Status = "Error", Message = "Already Exists" };
            }

            StatusVM st = await _statusReporsitory.addStatus(statusVM);
            ResponseVM responseVM = new ResponseVM { Status = "Success", Message = "Added Successfully" };
            return responseVM;
        }

        public async Task<bool> checkbyId(StatusVM statusVM)
        {
            bool alreadyExistId = await _statusReporsitory.checkStatusId(statusVM);
            if (alreadyExistId)
            {
                return true;
            } else
            {
                return false
            }
        }

        public async Task<bool> checkStatusAlreadyExists(StatusVM statusVM)
        {
            return await _statusReporsitory.checkStatus(statusVM);
        }
        public async Task<List<StatusVM>> GetAllStatus()
        {
            return await _statusReporsitory.getall();
        }
        public async Task<ResponseVM> UpdateStatus(StatusVM statusVM)
        {
            bool alreadyExist = await this.checkbyId(statusVM);
            if (alreadyExist) 
            {
                StatusVM vM = await _statusReporsitory.updateStatus(statusVM);
                return new ResponseVM { Status = "Success", Message = "Role Updated Successfully" };
            }
                return new ResponseVM { Status = "Error", Message = "Role Not Present" };
        }
    }
}
