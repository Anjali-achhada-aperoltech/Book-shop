using Book.Business.DTO;
using Book.Domain.Models;
using Book.Interfaces.Services;
using Book.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ROMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Book.Services
{
    public class BookItemsService : ServiceBase,IBookItemService
    {
        private readonly UserManager<Users> _userManager;
        public BookItemsService(IUnitOfWork unitOfWork,UserManager<Users>userManager) : base(unitOfWork)
        {
            _userManager = userManager;
        }

        public async Task<BookItemsDTO> AddAsync(BookItemsDTO model)
        {
            model.Id=Guid.NewGuid();
            BookItems b1 = new BookItems();
            b1.Name = model.Name;
            b1.Description = model.Description;
            b1.AuthorName= model.AuthorName;
            b1.price = model.price;
            b1.CategoryId = model.CategoryId;
            b1.FrontImage= model.FrontImage;
            var data = await unitOfWork.bookItemsRepositiory.AddAsync(b1);
            return model;

        }
       

        public  async Task<bool> DeleteAsync(Guid id)
        {
            var data = await unitOfWork.bookItemsRepositiory.GetAsync(id);
            if (data != null && id != null)
            {
                data.IsDeleted = true;
                var val = await unitOfWork.bookItemsRepositiory.DeleteAsync(data);
                return true;
            }

            else
            {
                return false;
            }
        }

        public async Task<List<CreateBookDTO>> GetAllAsync()
        {
           List<CreateBookDTO> list = new List<CreateBookDTO>();
            var data = await unitOfWork.bookItemsRepositiory.FindByAsync(x => !x.IsDeleted,includeProperties:"Category");
            foreach(var item in data)
            {
                list.Add(new CreateBookDTO { Id=item.Id, Name=item.Name, Description=item.Description,AuthorName=item.AuthorName,price=item.price,FrontImage=item.FrontImage,CategoryName=item.Category.Name});
            }
            return list;
        }

        public async Task<CreateBookDTO> GetAsync(Guid id)
        {
            var data = await unitOfWork.bookItemsRepositiory.GetAsync(id,includeProperties:"Category");
            CreateBookDTO vm = new CreateBookDTO();
            vm.Name= data.Name;
            vm.Description= data.Description;
            vm.price = data.price;
            vm.FrontImage= data.FrontImage;
            vm.AuthorName = data.AuthorName;
            vm.CategoryName = data.Category.Name;
            return vm;
            
        }

        public async Task<BookItemsDTO> GetItemsAsync(Guid id)
        {
            var data = await unitOfWork.bookItemsRepositiory.GetAsync(id);
            BookItemsDTO vm = new BookItemsDTO();
            vm.Name = data.Name;
            vm.Description = data.Description;
            vm.price = data.price;
            vm.FrontImage = data.FrontImage;
            vm.AuthorName = data.AuthorName;
            vm.CategoryId = data.CategoryId;
            return vm;
        }

        public async Task<bool> UpdateAsync(BookItemsDTO model)
        {
            try
            {
                var data = await unitOfWork.bookItemsRepositiory.GetAsync(model.Id);
                data.Name = model.Name;
                data.Description = model.Description;
                data.price = model.price;
                data.AuthorName = model.AuthorName;
                data.FrontImage = model.FrontImage;
                data.UpdatedDate = DateTime.Now;
                data.CategoryId = model.CategoryId;
                await unitOfWork.bookItemsRepositiory.UpdateAsync(data);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);

            }
            return await Task.FromResult(false);

        }
    }
    }

