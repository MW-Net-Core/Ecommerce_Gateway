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
    public class ProductController : ControllerBase
    {
        private readonly IProductManager _IProductManager;
        public ProductController(IProductManager iProductManager)
        {
            _IProductManager = iProductManager;
        }


        [HttpPost]
        [Route("Add-Product")]
        public async Task<IActionResult> addProduct([FromBody] ProductVM productVM) 
        {
            var res = await _IProductManager.InsertProduct(productVM);
            return Ok(res);
        }



        [HttpPut]
        [Route("Edit-Product")]
        public async Task<IActionResult> updateProduct([FromBody] ProductVM productVM)
        {
            var res = await _IProductManager.EditProduct(productVM);
            return Ok(res);
        }

    }
}
