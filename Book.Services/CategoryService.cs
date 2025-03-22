using Book.Business.DTO;
using Book.Interfaces.Services;
using Book.UOW;
using Book_Shop.Models;
using ROMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Services
{
    public class CategoryService : ServiceBase, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<CategoryDto> AddAsync(CategoryDto model)
        {
            var existingcategory = await unitOfWork.CategoryReposititory.FindByAsync(x => x.Name.ToLower() == model.Name.ToLower());
            if (existingcategory != null)
            {
                return null;
            }
            model.Id = Guid.NewGuid();
            Category category = new Category();
            category.Name = model.Name;
            category.Description = model.Description;
            await unitOfWork.CategoryReposititory.AddAsync(category);
            return model;

        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var data = await unitOfWork.CategoryReposititory.GetAsync(id);
            if (data != null)
            {
                data.IsDeleted = true;
                await unitOfWork.CategoryReposititory.DeleteAsync(data);
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }

        public async Task<List<CategoryDto>> GetAllAsync()
        {
            List<CategoryDto> vm = new List<CategoryDto>();
            var data = await unitOfWork.CategoryReposititory.FindByAsync(x => !x.IsDeleted);
            foreach (var item in data) {
                vm.Add(new CategoryDto { Id = item.Id, Name = item.Name, Description = item.Description });
            }
            return vm;
        }

        public async Task<CategoryDto> GetAsync(Guid id)
        {
            try
            {
                var data = await unitOfWork.CategoryReposititory.GetAsync(id);
                
                    CategoryDto vm = new CategoryDto();
                    vm.Name = data.Name;
                    vm.Description = data.Description;
                    return vm;
                
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }

        public async Task<bool> UpdateAsync(CategoryDto model)
        {
            
            try
            {
                var existingCategory = await unitOfWork.CategoryReposititory
            .FindByAsync(c => c.Name == model.Name && c.Id != model.Id);

                if (existingCategory != null)
                {
                    return false; 
                }

                var data = await unitOfWork.CategoryReposititory.GetAsync(model.Id);
                data.Name = model.Name;
                data.Description = model.Description;
                await unitOfWork.CategoryReposititory.UpdateAsync(data);
                return await Task.FromResult(true) ;
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);

            }
            return await Task.FromResult(false);

        }
    }
}

