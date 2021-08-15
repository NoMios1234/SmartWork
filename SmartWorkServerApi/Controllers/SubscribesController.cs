using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartWork.Core.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWorkServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribesController : ControllerBase
    {
        private readonly ISubscribeService _subscribeService;
        
        public SubscribesController(ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;
        }

        // POST: api/Subscribes/SubscribeUser
        [HttpPost("SubscribeUser")]
        public Task<ActionResult> SubscribeUser(string userId, int subscribeDetailsId)
        {
            return _subscribeService.SubscribeUserAsync(userId, subscribeDetailsId);
        }
    }
}
