using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SmartWork.Core.Abstractions.Services
{
    public interface ISubscribeService
    {
        Task<ActionResult> SubscribeUserAsync(string userId, int subscribeDetailId);
    }
}
