using Book.Business.DTO;
using Book.Domain.Models;
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
    public class SubCategoryService : ServiceBase,ISubCategoryService
    {
        public SubCategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<SubCategoryDTO> AddAsync(SubCategoryDTO model)
        {
            var existingsubcategory = await unitOfWork.SubCategoryRepository.FindByAsync(x => x.Name.ToLower() == model.Name.ToLower());
            if (existingsubcategory != null && existingsubcategory.Any())
            {
                return null;
            }
            model.Id = Guid.NewGuid();
            SubCategory subCategory = new SubCategory();
            subCategory.Name = model.Name;
            subCategory.Description = model.Description;
            subCategory.CategoryId = model.CategoryId;
            await unitOfWork.SubCategoryRepository.AddAsync(subCategory);               


            return model;

        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var data = await unitOfWork.SubCategoryRepository.GetAsync(id);
            if (data != null)
            {
                data.IsDeleted = true;
                await unitOfWork.SubCategoryRepository.DeleteAsync(data);
                return true;
            }
            else
            {
                return false;
            }           
        }
        public async Task<List<SubCategoryDTO>> GetAllAsync()
        {
            List<SubCategoryDTO> vm = new List<SubCategoryDTO>();
            var data = await unitOfWork.SubCategoryRepository.FindByAsync(x => !x.IsDeleted);
            foreach (var item in data)
            {
                vm.Add(new SubCategoryDTO { Id = item.Id, Name = item.Name, Description = item.Description, Category = item.Category, CategoryId = item.CategoryId });
            }
            return vm;
        }
        public async Task<List<CategoryDto>> GetCategorySelectList()
        {
            List<CategoryDto> vm = new List<CategoryDto>();
            var data = await unitOfWork.CategoryReposititory.FindByAsync(x => !x.IsDeleted);
            foreach (var item in data)
            {
                vm.Add(new CategoryDto { Id = item.Id, Name = item.Name, Description = item.Description });
            }
            return vm;
        }
        public async Task<SubCategoryDTO> GetAsync(Guid id)
        {
            try
            {
                var data = await unitOfWork.SubCategoryRepository.GetAsync(id);

                SubCategoryDTO vm = new SubCategoryDTO();
                vm.Name = data.Name;
                vm.Description = data.Description;
                vm.CategoryId = data.CategoryId;
                return vm;

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<bool> UpdateAsync(SubCategoryDTO model)
        {
            try
            {
                var existingsubCategory = await unitOfWork.SubCategoryRepository
            .FindByAsync(c => c.Name == model.Name && c.Id != model.Id);

                if (existingsubCategory != null && existingsubCategory.Any())
                {
                    return false;
                }
                var data = await unitOfWork.SubCategoryRepository.GetAsync(model.Id);
                data.Name = model.Name;
                data.Description = model.Description;
                data.CategoryId = model.CategoryId;
                await unitOfWork.SubCategoryRepository.UpdateAsync(data);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
