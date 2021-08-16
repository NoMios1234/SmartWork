using Microsoft.AspNetCore.Mvc;
using SmartWork.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartWork.Core.Abstractions.Services
{
    public interface ISubscribeService
    {
        Task<ActionResult> SubscribeUserAsync(string userId, int subscribeDetailId);
        Task<IEnumerable<Subscribe>> UserSubscribesAsync(string userId);
    }
}
