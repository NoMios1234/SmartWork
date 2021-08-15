using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartWork.Core.Entities;
using SmartWork.Core.Specifications;
using SmartWork.Core.ViewModels.OfficeViewModels;
using SmartWork.Core.ViewModels.SubscribeDetailViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SmartWork.Core.Abstractions.Services
{
    public interface IOfficeService
    {
        Task<IEnumerable<OfficeViewModel>> GetOfficesAsync(Specification<Office> specification);
        Task<IEnumerable<OfficeViewModel>> GetCompanyOfficesAsync(int id);
        Task<IEnumerable<SubscribeDetailViewModel>> GetOfficeSubscribeDetailsAsync(int id);
        Task<OfficeViewModel> FindOfficeAsync(int id);
        Task<ActionResult> AddOfficeAsync(AddOfficeViewModel model);
        Task<ActionResult> SubscribeUserToOfficeAsync(string userId, int subscribeDetailsId);
        Task<ActionResult> UpdateOfficeAsync(UpdateOfficeViewModel model);
        Task<ActionResult> DeleteOfficeAsync(int id);
        string SaveFile(HttpRequest request);
    }
}
