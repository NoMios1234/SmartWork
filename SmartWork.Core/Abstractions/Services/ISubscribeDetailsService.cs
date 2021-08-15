using Microsoft.AspNetCore.Mvc;
using SmartWork.Core.Entities;
using SmartWork.Core.Specifications;
using SmartWork.Core.ViewModels.SubscribeDetailViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartWork.Core.Abstractions.Services
{
    public interface ISubscribeDetailsService
    {
        Task<IEnumerable<SubscribeDetailViewModel>> GetSubscribesAsync(Specification<SubscribeDetail> specification);
        Task<IEnumerable<SubscribeDetailViewModel>> GetOfficeSubscribesAsync(int id);
        Task<SubscribeDetailViewModel> FindSubscribeAsync(int id);
        Task<ActionResult> AddSubscribeToOfficeAsync(AddSubscribeDetailViewModel model);
        Task<ActionResult> UpdateSubscribeAsync(UpdateSubscribeDetailViewModel model);
        Task<ActionResult> DeleteSubscribeFromOfficeAsync(int id);
    }
}
