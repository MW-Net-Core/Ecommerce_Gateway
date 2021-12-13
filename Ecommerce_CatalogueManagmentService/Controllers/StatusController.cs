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

        [HttpGet]
        [Route("get-all-status")]
        public async Task<IActionResult> getAllStatus() 
        {
            var res = await _status.GetAllStatus();
            return Ok(res);
        }
       
        
        [HttpPost]
        [Route("add-status")]
        public async Task<IActionResult> addStatus([FromBody]StatusVM statusVM) 
        {
            var res = await _status.AddStatus(statusVM);
            return Ok(res);
        }
        
        [HttpPut]
        [Route("update-status")]
        public async Task<IActionResult> updateStatus([FromBody] StatusVM statusVM)
        {
            var res = await _status.UpdateStatus(statusVM);
            return Ok(res);
        }

        [HttpDelete]
        [Route("delete-status")]
        public async Task<IActionResult> deleteStatus(Guid? id)
        {
            var res = await _status.DeleteStatus(id);
            return Ok(res);
        }

    }
}
