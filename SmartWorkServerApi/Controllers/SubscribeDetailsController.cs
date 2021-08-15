using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartWork.Core.Abstractions.Services;
using SmartWork.Core.Entities;
using SmartWork.Core.ViewModels.SubscribeDetailViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWorkServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribeDetailsController : ControllerBase
    {
        private readonly ISubscribeDetailsService _subscribeDetailsService;
        public SubscribeDetailsController(ISubscribeDetailsService subscribeDetailsService)
        {
            _subscribeDetailsService = subscribeDetailsService;
        }

        // GET: api/SubscribeDetails/GetSubscribes
        [HttpGet("GetSubscribes")]
        public Task<IEnumerable<SubscribeDetailViewModel>> GetSubscribes()
        {
            return _subscribeDetailsService.GetSubscribesAsync(new SmartWork.Core.Specifications
                .Specification<SubscribeDetail>(c => c.Id != -1));
        }

        // POST: api/SubscribeDetails/AddSubscribeToOffice
        [HttpPost("AddSubscribeToOffice")]
        public Task<ActionResult> AddSubscribeToOffice(AddSubscribeDetailViewModel model)
        {
            return _subscribeDetailsService.AddSubscribeToOfficeAsync(model);
        }
    }
}
