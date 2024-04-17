using Book.Business.DTO;
using Book.Domain.Models;
using ROMS.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Interfaces.Services
{
    public interface IOrderHeaderService
    {
        Task<CartVm>SummeryPage(CartVm cartDto);
    }
}
