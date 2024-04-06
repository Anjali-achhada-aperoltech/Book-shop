using Book.UOW;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROMS.Services
{
    public abstract class ServiceBase : IDisposable
    {
        protected readonly IUnitOfWork unitOfWork;
       
        public string DeviceType;

        protected ServiceBase(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing) unitOfWork.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ServiceBase()
        {
            Dispose(false);
        }
    }
}
