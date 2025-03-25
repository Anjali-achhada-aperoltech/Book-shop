using Book.Business.DTO;
using Book_Shop.Models;
using ROMS.Interfaces.Repositories;
using ROMS.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Interfaces.Services
{
    public interface ISubCategoryService:IBaseService<SubCategoryDTO>
    {
        Task<List<CategoryDto>> GetCategorySelectList();
       
    }
}
