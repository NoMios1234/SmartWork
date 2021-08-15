using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartWork.Core.Abstractions.Repositories;
using SmartWork.Core.Abstractions.Services;
using SmartWork.Core.Entities;
using SmartWork.Core.Specifications;
using SmartWork.Core.ViewModels.SubscribeViewModels;
using System;
using System.Threading.Tasks;

namespace SmartWork.BLL.Services
{
    public class SubscribeService : ISubscribeService
    {
        private readonly IRepository<Subscribe> _repository;
        private readonly ILogger<SubscribeService> _logger;
        private readonly ISubscribeDetailsService _subscribeDetailsService;

        public SubscribeService(IRepository<Subscribe> repository, ILogger<SubscribeService> logger,
            ISubscribeDetailsService subscribeDetailsService)
        {
            _repository = repository;
            _logger = logger;
            _subscribeDetailsService = subscribeDetailsService;
        }

        public async Task<ActionResult> SubscribeUserAsync(string userId, int subscribeDetailId)
        {
            try
            {
                var subscribeDetails = await _subscribeDetailsService.FindSubscribeAsync(subscribeDetailId);
                var subscribe = new Subscribe()
                {
                    StartSubscribe = DateTime.Now,
                    EndSubscribe = DateTime.Now,
                    SubscribeDescription = subscribeDetails.SubscribeDescription,
                    SubscribeDetailId = subscribeDetailId,
                    SubscribeName = subscribeDetails.SubscribeName,
                    UserId = userId
                };

                await _repository.AddAsync(subscribe);

                subscribe = await _repository.FindAsync(specification: new Specification<Subscribe>(
                        s => s.SubscribeDetailId == subscribeDetailId && s.UserId == userId));

                return new OkObjectResult(JsonConvert.DeserializeObject<SubscribeViewModel>
                    (JsonConvert.SerializeObject(subscribe)));
            }
            catch(Exception ex)
            {
                _logger.LogError("SubscribeService: SubscribeUserAsync\n" + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
