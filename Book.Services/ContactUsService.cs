using Book.Business.DTO;
using Book.Domain.Models;
using Book.Interfaces.Services;
using Book.UOW;
using ROMS.Services;

namespace Book.Services
{
    public class ContactUsService : ServiceBase, IContactUsService
    {
        public ContactUsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ContactUsDTO> AddAsync(ContactUsDTO model)
        {
            model.Id = Guid.NewGuid();
            ContactUs c1 = new ContactUs();
            c1.Name = model.Name;
            c1.Email = model.Email;
            c1.Subject = model.Subject;
            c1.Message = model.Message;
            await unitOfWork.contactUsRepository.AddAsync(c1);
            return model;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var data = await unitOfWork.contactUsRepository.GetAsync(id);
            if (data != null)
            {
                data.IsDeleted = true;
                await unitOfWork.contactUsRepository.DeleteAsync(data);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<ContactUsDTO>> GetAllAsync()
        {
            List<ContactUsDTO> vm = new List<ContactUsDTO>();
            var data = await unitOfWork.contactUsRepository.FindByAsync(x => !x.IsDeleted);
            foreach (var item in data)
            {
                vm.Add(new ContactUsDTO { Id = item.Id, Name = item.Name, Email = item.Email, Subject = item.Subject, Message = item.Message });
            }
            return vm;
        }

        public async Task<ContactUsDTO> GetAsync(Guid id)
        {
            try
            {
                var data = await unitOfWork.contactUsRepository.GetAsync(id);

                ContactUsDTO vm = new ContactUsDTO();
                vm.Name = data.Name;
                vm.Email = data.Email;
                vm.Subject = data.Subject;
                vm.Message = data.Message;
                return vm;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(ContactUsDTO model)
        {
            try
            {
                var data = await unitOfWork.contactUsRepository.GetAsync(model.Id);
                data.Name = model.Name;
                data.Email = model.Email;
                data.Subject = model.Subject;
                data.Message = model.Message;
                await unitOfWork.contactUsRepository.UpdateAsync(data);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
            
        }

    }
}