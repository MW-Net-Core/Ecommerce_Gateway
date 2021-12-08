using Ecommerce_CatalogueManagmentService.Business.BAL.Interface;
using Ecommerce_CatalogueManagmentService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_CatalogueManagmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductStatusController : ControllerBase
    {
        private readonly IProductStatusManager _productStatusManager;
        public ProductStatusController(IProductStatusManager productStatusManager)
        {
            _productStatusManager = productStatusManager;
        }

        [HttpPost]
        [Route("add-product-status")]
        public async Task<IActionResult> add([FromBody] StatusProductVM statusProductVM)
        {
            var res = await _productStatusManager.addStatusProduct(statusProductVM);
            return Ok(res);
        }


        [HttpPut]
        [Route("edit-product-status")]
        public async Task<IActionResult> edit([FromBody] StatusProductVM statusProductVM)
        {
            var res = await _productStatusManager.editStatusProduct(statusProductVM);
            return Ok(res);
        }


        [HttpGet]
        [Route("get-All")]
        public async Task<IActionResult> getAll()
        {
            var res = await _productStatusManager.getAllProductsWithStatus();
            return Ok(res);
        }

    }
}