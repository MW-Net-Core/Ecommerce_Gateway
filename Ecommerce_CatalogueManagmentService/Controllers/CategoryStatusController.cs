using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_CatalogueManagmentService.Business.BAL;
using Ecommerce_CatalogueManagmentService.Models;
using Ecommerce_CatalogueManagmentService.Business.BAL.Interface;

namespace Ecommerce_CatalogueManagmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryStatusController : ControllerBase
    {
        private readonly ICategoryStatusManager _categoryStatusManager;
        public CategoryStatusController(ICategoryStatusManager categoryStatusManager)
        {
            _categoryStatusManager = categoryStatusManager;
        }
    
        [HttpPost]
        [Route("add-Category-status")]
        public async Task<IActionResult> add([FromBody] StatusCategoryVM statusCategoryVM)
        {
            var res = await _categoryStatusManager.addStatusCategory(statusCategoryVM);
            return Ok(res);
        }


        [HttpPut]
        [Route("edit-Category-status")]
        public async Task<IActionResult> edit([FromBody] StatusCategoryVM statusCategoryVM)
        {
            var res = await _categoryStatusManager.editStatusCategory(statusCategoryVM);
            return Ok(res);
        }


    }
}
