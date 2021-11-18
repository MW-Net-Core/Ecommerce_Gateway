using Ecommerce_CatalogueManagmentService.Entities.DO;
using Ecommerce_CatalogueManagmentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_CatalogueManagmentService.Repository.DAL.Interfaces
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CategoryExist(CategoryVM categoryVM)
        {
            var categoryExist = await _context.TblCategory.Where(x => x.CategoryName == categoryVM.CategoryName).FirstOrDefaultAsync();
            if(categoryExist != null)
                return true;
            return false;
            
        }
        public async Task<bool> checkCategoryId(Guid? id)
        {
            var categoryIsAvailable = await _context.TblCategory.Where(x => x.CategoryId == id).FirstOrDefaultAsync();
            if (categoryIsAvailable != null)
                return true;
            return false;
        }
        public async Task<bool> checkStatusId(Guid? id)
        {
            var statusIsAvailable = await _context.TblStatus.Where(x => x.StatusId == id).FirstOrDefaultAsync();
            if (statusIsAvailable != null)
                return true;
            return false;
        }
        public async Task<CategoryVM> AddCategory(CategoryVM categoryVM)
        {


            Category category = new Category
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = categoryVM.CategoryName,
                CategoryDescription = categoryVM.CategoryDescription,
                StatusId = categoryVM.StatusId
            };



            try
            {
                var result = await _context.TblCategory.AddAsync(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }





            return categoryVM;

        }
        public async Task<CategoryVM> UpdateCategory(CategoryVM categoryVM)
        {
            var category = await _context.TblCategory.FirstOrDefaultAsync(o => o.CategoryId.Equals(categoryVM.CategoryId));
            if (category != null)
            {
                category.CategoryName = categoryVM.CategoryName;
                category.CategoryDescription = categoryVM.CategoryDescription;
                category.StatusId = categoryVM.StatusId;

                _context.Entry(category).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return categoryVM;
            }
            else
            {
                CategoryVM vM = new CategoryVM
                {
                    CategoryId = Guid.NewGuid(),
                    CategoryName = ""
                };
                return vM;
            }
        }
        public async Task<List<CategoryVM>> GetAllCategories()
        {
            return await _context.TblCategory
               .Select(o => new CategoryVM
               {
                   CategoryId = o.CategoryId,
                   CategoryName = o.CategoryName,
                   CategoryDescription = o.CategoryDescription,
                   StatusId = o.StatusId                 
                    //}).Where(x => x.StatusId == Guid.NewGuid()).ToListAsync();
               }).ToListAsync();
        }
    }
}