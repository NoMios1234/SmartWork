using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartWork.Core.Abstractions.Repositories;
using SmartWork.Core.Abstractions.Services;
using SmartWork.Core.Entities;
using SmartWork.Core.Specifications;
using SmartWork.Core.ViewModels.SubscribeDetailViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartWork.BLL.Services
{
    public class SubscribeDetailsService : ISubscribeDetailsService
    {
        // READONLY
        private readonly IRepository<SubscribeDetail> _repository;
        private readonly ILogger<SubscribeDetailsService> _logger;

        // CONSTANTS
        const string NULL_RESULT = "object not found in database";

        public SubscribeDetailsService(IRepository<SubscribeDetail> repository,
            ILogger<SubscribeDetailsService> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        // GET Subscribes
        public async Task<IEnumerable<SubscribeDetailViewModel>> GetSubscribesAsync(Specification<SubscribeDetail> specification)
        {
            return JsonConvert.DeserializeObject<IEnumerable<SubscribeDetailViewModel>>(
                JsonConvert.SerializeObject(await _repository.GetAsync(specification)));
        }

        // GET OfficeSubscribes
        public async Task<IEnumerable<SubscribeDetailViewModel>> GetOfficeSubscribesAsync(int id)
        {
            var specification = new Specification<SubscribeDetail>(s => s.OfficeId == id);
            return JsonConvert.DeserializeObject<IEnumerable<SubscribeDetailViewModel>>(
                JsonConvert.SerializeObject(await _repository.GetAsync(specification)));
        }

        // FIND Subscribe
        public async Task<SubscribeDetailViewModel> FindSubscribeAsync(int id)
        {
            return JsonConvert.DeserializeObject<SubscribeDetailViewModel>(
                JsonConvert.SerializeObject(await _repository.FindAsync(id)));
        }

        // ADD SubscribeToOffice
        public async Task<ActionResult> AddSubscribeToOfficeAsync(AddSubscribeDetailViewModel model)
        {
            try
            {
                var subscribeDetail = new SubscribeDetail
                {
                    SubscribeName = model.SubscribeName,
                    SubscribePrice = model.SubscribePrice,
                    SubscribeDescription = model.SubscribeDescription,
                    OfficeId = model.OfficeId
                };

                await _repository.AddAsync(subscribeDetail);

                subscribeDetail = await _repository.FindAsync(specification: new Specification<SubscribeDetail>(c =>
                    c.SubscribeName == model.SubscribeName && c.SubscribePrice == model.SubscribePrice &&
                    c.OfficeId == model.OfficeId));
                return new OkObjectResult(JsonConvert.DeserializeObject<SubscribeDetailViewModel>
                    (JsonConvert.SerializeObject(subscribeDetail)));
            }
            catch(Exception ex)
            {
                _logger.LogError("SubscribeDetailService: AddSubscribeToOfficeAsync\n" + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // UPDATE Subscribe
        public async Task<ActionResult> UpdateSubscribeAsync(UpdateSubscribeDetailViewModel model)
        {
            try
            {
                var subscribeDetail = await _repository.FindAsync(model.Id);

                subscribeDetail.SubscribeName = model.SubscribeName;
                subscribeDetail.SubscribePrice = model.SubscribePrice;
                subscribeDetail.SubscribeDescription = model.SubscribeDescription;
                subscribeDetail.OfficeId = model.OfficeId;

                await _repository.UpdateAsync(subscribeDetail);

                return new OkObjectResult(JsonConvert.DeserializeObject<SubscribeDetailViewModel>
                    (JsonConvert.SerializeObject(subscribeDetail)));
            }
            catch (Exception ex)
            {
                _logger.LogError("SubscribeDetailService: AddSubscribeToOfficeAsync\n" + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // DELETE SubscribeFromOffice
        public Task<ActionResult> DeleteSubscribeFromOfficeAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
