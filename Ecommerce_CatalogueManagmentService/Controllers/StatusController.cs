using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_CatalogueManagmentService.Business.BAL;
using Ecommerce_CatalogueManagmentService.Business.BAL.Interface;
using Ecommerce_CatalogueManagmentService.Models;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce_CatalogueManagmentService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusManager _status;
        public StatusController(IStatusManager status)
        {
            _status = status;
        }

        [HttpGet(nameof(getAllStatus))]
        public async Task<IActionResult> getAllStatus() 
        {
            var res = await _status.GetAllStatus();
            return Ok(res);
        }
       
        
        [HttpPost(nameof(addStatus))]
        public async Task<IActionResult> addStatus([FromBody]StatusVM statusVM) 
        {
            var res = await _status.AddStatus(statusVM);
            return Ok(res);
        }
        
        [HttpPut(nameof(updateStatus))]
        public async Task<IActionResult> updateStatus([FromBody] StatusVM statusVM)
        {
            var res = await _status.UpdateStatus(statusVM);
            return Ok(res);
        }

        [HttpDelete(nameof(deleteStatus))]
        public async Task<IActionResult> deleteStatus(Guid? id)
        {
            var res = await _status.DeleteStatus(id);
            return Ok(res);
        }

    }
}
