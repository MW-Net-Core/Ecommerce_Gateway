using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_CatalogueManagmentService.Business.BAL;
using Ecommerce_CatalogueManagmentService.Models;
using System.Threading;

namespace Ecommerce_CatalogueManagmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;

        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpPost]
        [Route("add-category")]
        public async Task<IActionResult> addCategory([FromBody] CategoryVM categoryVM)
        {
            var res = await _categoryManager.AddCategory(categoryVM);
            return Ok(res);
        }

        [HttpPut]
        [Route("update-category")]
        public async Task<IActionResult> updateCategory([FromBody] CategoryVM categoryVM)
        {
            var res = await _categoryManager.UpdateCategory(categoryVM);
            return Ok(res);
        }

        [HttpGet]
        [Route("get-all-category")]
        public async Task<IActionResult> getAllCategories()
        {
            var Catergory = Thread.CurrentPrincipal.Identity;
            var isEnrolled = Thread.CurrentPrincipal.IsInRole("User");
            var res = await _categoryManager.GetAllCategories();
            return Ok(res);
        }







        [HttpDelete]
        [Route("delete-category")]
        public async Task<IActionResult> DeleteCategory(Guid? id)
        {
            var res = await _categoryManager.DeleteCategorys(id);
            return Ok(res);

        }


    }
}
