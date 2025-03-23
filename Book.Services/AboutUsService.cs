using Book.Business.DTO;
using Book.Domain.Models;
using Book.Interfaces.Services;
using Book.UOW;
using ROMS.Services;


namespace Book.Services
{
    public class AboutUsService : ServiceBase, IAboutUsService
    {
        public AboutUsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<AboutUsDto> AddAsync(AboutUsDto model)
        {
            model.Id = Guid.NewGuid();
            AboutUs b1 = new AboutUs();
            b1.PageTitle = model.PageTitle;
            b1.PageDescription=model.PageDescription;
            await unitOfWork.aboutUsRepostitory.AddAsync(b1);
            return model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {

            var data = await unitOfWork.aboutUsRepostitory.GetAsync(id);
            if (data != null)
            {
                data.IsDeleted = true;
                await unitOfWork.aboutUsRepostitory.DeleteAsync(data);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<AboutUsDto>> GetAllAsync()
        {
            List<AboutUsDto> vm = new List<AboutUsDto>();
            var data = await unitOfWork.aboutUsRepostitory.FindByAsync(x => !x.IsDeleted);
            foreach (var item in data)
            {
                vm.Add(new AboutUsDto { Id = item.Id,PageTitle = item.PageTitle, PageDescription = item.PageDescription });
            }
            return vm;
        }

        public async Task<AboutUsDto> GetAsync(Guid id)
        {
            try
            {
                var data = await unitOfWork.aboutUsRepostitory.GetAsync(id);

                AboutUsDto vm = new AboutUsDto();
                vm.PageTitle = data.PageTitle;
                vm.PageDescription = data.PageDescription;
                return vm;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> UpdateAsync(AboutUsDto model)
        {
            try
            {
                var data = await unitOfWork.aboutUsRepostitory.GetAsync(model.Id);
                data.PageTitle = model.PageTitle;
                data.PageDescription = model.PageDescription;
                await unitOfWork.aboutUsRepostitory.UpdateAsync(data);
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
