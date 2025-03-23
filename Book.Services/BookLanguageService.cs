using Book.Business.DTO;
using Book.Domain.Models;
using Book.Interfaces.Services;
using Book.UOW;
using Book_Shop.Models;
using ROMS.Interfaces.Services;
using ROMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Services
{
    public class BookLanguageService :ServiceBase, IBookLanguageService
    {
        public BookLanguageService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<BookLangageDTO> AddAsync(BookLangageDTO model)
        {
            var existingCategory = await unitOfWork.bookLanguageRepostiory.FindByAsync(x => x.Name.ToLower() == model.Name.ToLower());

            if (existingCategory != null && existingCategory.Any()) // If it returns a list
            {
                return null;
            }

            model.Id = Guid.NewGuid();
            BookLanguage b1 = new BookLanguage();
            b1.Name = model.Name;
            await unitOfWork.bookLanguageRepostiory.AddAsync(b1);
            return model;
        }

        public  async Task<bool> DeleteAsync(Guid id)
        {
            var data = await unitOfWork.bookLanguageRepostiory.GetAsync(id);
            if (data != null)
            {
                data.IsDeleted = true;
                await unitOfWork.bookLanguageRepostiory.DeleteAsync(data);
                return true;
            }
            else
            {
                return false;
            }
            return false;
        }

        public async Task<List<BookLangageDTO>> GetAllAsync()
        {
            List<BookLangageDTO> vm = new List<BookLangageDTO>();
            var data = await unitOfWork.bookLanguageRepostiory.FindByAsync(x => !x.IsDeleted);
            foreach (var item in data)
            {
                vm.Add(new BookLangageDTO { Id = item.Id, Name = item.Name });
            }
            return vm;
        }

        public async Task<BookLangageDTO> GetAsync(Guid id)
        {
            try
            {
                var data = await unitOfWork.bookLanguageRepostiory.GetAsync(id);

                BookLangageDTO vm = new BookLangageDTO();
                vm.Name = data.Name;
                return vm;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(BookLangageDTO model)
        {
            try
            {
                var existingCategory = await unitOfWork.bookLanguageRepostiory
            .FindByAsync(c => c.Name == model.Name && c.Id != model.Id);

                if (existingCategory != null && existingCategory.Any())
                {
                    return false;
                }

                var data = await unitOfWork.bookLanguageRepostiory.GetAsync(model.Id);
                data.Name = model.Name;
                await unitOfWork.bookLanguageRepostiory.UpdateAsync(data);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);

            }
        }
    }
}
    

