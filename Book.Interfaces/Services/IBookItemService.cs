﻿using Book.Business.DTO;
using ROMS.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Interfaces.Services
{
    public interface IBookItemService
    {
        Task<List<CreateBookDTO>> GetAllAsync();
        Task<List<CreateBookDTO>> GetAllCategoryAsync(Guid? CategoryId=null);

        Task<CreateBookDTO> GetAsync(Guid id);
        Task<BookItemsDTO> GetItemsAsync(Guid id);

        Task<BookItemsDTO> AddAsync(BookItemsDTO model);

        Task<bool> UpdateAsync(BookItemsDTO model);

        Task<bool> DeleteAsync(Guid id);
        Task<List<CategoryDto>> SelctCategoryList();
    }
}
