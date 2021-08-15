using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartWork.Core.Abstractions.Repositories;
using SmartWork.Core.Abstractions.Services;
using SmartWork.Core.Entities;
using SmartWork.Core.Specifications;
using SmartWork.Core.ViewModels.OfficeViewModels;
using SmartWork.Core.ViewModels.SubscribeDetailViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SmartWork.BLL.Services
{
    public class OfficeService : IOfficeService
    {
        // READONLY
        private readonly IRepository<Office> _repository;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<OfficeService> _logger;
        private readonly ISubscribeDetailsService _subscribeDetailsService;
        private readonly ISubscribeService _subscribeService;


        // CONSTANTS
        const string NULL_RESULT = "object not found in database";

        public OfficeService(IRepository<Office> repository, IWebHostEnvironment env,
            ILogger<OfficeService> logger, ISubscribeDetailsService subscribeDetailsService,
            ISubscribeService subscribeService)
        {
            _repository = repository;
            _env = env;
            _logger = logger;
            _subscribeDetailsService = subscribeDetailsService;
            _subscribeService = subscribeService;
        }

        // GET Offices
        public async Task<IEnumerable<OfficeViewModel>> GetOfficesAsync(Specification<Office> specification)
        {
            return JsonConvert.DeserializeObject<IEnumerable<OfficeViewModel>>(
                JsonConvert.SerializeObject(await _repository.GetAsync(specification)));
        }

        // GET CompanyOffices
        public async Task<IEnumerable<OfficeViewModel>> GetCompanyOfficesAsync(int id)
        {
            var specification = new Specification<Office>(o => o.CompanyId == id);
            return JsonConvert.DeserializeObject<IEnumerable<OfficeViewModel>>(
                JsonConvert.SerializeObject(await _repository.GetAsync(specification)));
        }

        // GET OfficeSubscribeDetails
        public Task<IEnumerable<SubscribeDetailViewModel>> GetOfficeSubscribeDetailsAsync(int id)
        {
            return _subscribeDetailsService.GetOfficeSubscribesAsync(id);
        }

        // FIND Office
        public async Task<OfficeViewModel> FindOfficeAsync(int id)
        {
            return JsonConvert.DeserializeObject<OfficeViewModel>(
                JsonConvert.SerializeObject(await _repository.FindAsync(id)));
        }

        // ADD Office
        public async Task<ActionResult> AddOfficeAsync(AddOfficeViewModel model)
        {
            try
            {
                var office = new Office
                {
                    OfficeName = model.OfficeName,
                    OfficeAddress = model.OfficeAddress,
                    OfficePhoneNumber = model.OfficePhoneNumber,
                    PhotoFileName = model.PhotoFileName,
                    IsFavourite = true,
                    CompanyId = model.CompanyId
                };

                await _repository.AddAsync(office);

                office = await _repository.FindAsync(specification: new Specification<Office>(
                    o => o.OfficeName == model.OfficeName && o.OfficeAddress == model.OfficeAddress &&
                    o.CompanyId == model.CompanyId));

                return new OkObjectResult(JsonConvert.DeserializeObject<OfficeViewModel>
                    (JsonConvert.SerializeObject(office)));
            }
            catch(Exception ex)
            {
                _logger.LogError("OfficeService: AddOfficeAsync\n" + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public Task<ActionResult> SubscribeUserToOfficeAsync(string userId, int subscribeDetailId)
        {
            return _subscribeService.SubscribeUserAsync(userId, subscribeDetailId);
        }

        // UPDATE Office
        public async Task<ActionResult> UpdateOfficeAsync(UpdateOfficeViewModel model)
        {
            try
            {
                var office = await _repository.FindAsync(model.Id);
                office.OfficeName = model.OfficeName;
                office.OfficeAddress = model.OfficeAddress;
                office.OfficePhoneNumber = model.OfficePhoneNumber;
                office.PhotoFileName = model.PhotoFileName;
                office.CompanyId = model.CompanyId;

                await _repository.UpdateAsync(office);

                return new OkObjectResult(JsonConvert.DeserializeObject<OfficeViewModel>
                    (JsonConvert.SerializeObject(office)));
            }
            catch (Exception ex)
            {
                _logger.LogError("OfficeService: UpdateOfficeAsync\n" + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // DELETE Office
        public Task<ActionResult> DeleteOfficeAsync(int id)
        {
            throw new NotImplementedException();
        }

        // ACTION SaveFiles
        public string SaveFile(HttpRequest request)
        {
            try
            {
                var httpRequest = request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/Office/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return filename;
            }
            catch (Exception ex)
            {
                _logger.LogError("OfficeService: SaveFile\n" + ex.Message);
                return "default_office_image.png";
            }
        }

        public Task<ActionResult> SubscribeUserToOfficeAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
