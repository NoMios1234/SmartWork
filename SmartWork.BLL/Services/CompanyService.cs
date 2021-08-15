using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartWork.Core.Abstractions.Repositories;
using SmartWork.Core.Abstractions.Services;
using SmartWork.Core.Entities;
using SmartWork.Core.Specifications;
using SmartWork.Core.ViewModels.CompanyViewModels;
using SmartWork.Core.ViewModels.OfficeViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SmartWork.BLL.Services
{
    public class CompanyService : ICompanyService
    {
        // READONLY
        private readonly IRepository<Company> _repository;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<CompanyService> _logger;
        private readonly IOfficeService _officeService;

        // CONSTANTS
        const string NULL_RESULT = "object not found in database";

        public CompanyService(IRepository<Company> repository, IWebHostEnvironment env,
            ILogger<CompanyService> logger, IOfficeService officeService)
        {
            _repository = repository;
            _env = env;
            _logger = logger;
            _officeService = officeService;
        }

        // GET Companies
        public async Task<IEnumerable<CompanyViewModel>> GetCompaniesAsync(Specification<Company> specification)
        {
            return JsonConvert.DeserializeObject<IEnumerable<CompanyViewModel>>(
                JsonConvert.SerializeObject(await _repository.GetAsync(specification)));
        }

        // GET CompanyOffices
        public async Task<IEnumerable<OfficeViewModel>> GetCompanyOfficesAsync(int id)
        {
            return await _officeService.GetCompanyOfficesAsync(id);
        }

        // ADD Company
        public async Task<ActionResult> AddCompanyAsync(AddCompanyViewModel model)
        {
            try
            {
                var company = new Company
                {
                    CompanyName = model.CompanyName,
                    CompanyAddress = model.CompanyAddress,
                    CompanyPhoneNumber = model.CompanyPhoneNumber,
                    CompanyDescription = model.CompanyDescription,
                    PhotoFileName = model.PhotoFileName
                };

                await _repository.AddAsync(company);

                company = await _repository.FindAsync(specification: new Specification<Company>(c => 
                    c.CompanyName == model.CompanyName && c.CompanyAddress == model.CompanyAddress));
                return new OkObjectResult(JsonConvert.DeserializeObject<CompanyViewModel>
                    (JsonConvert.SerializeObject(company)));
            }
            catch (Exception ex)
            {
                _logger.LogError("CompanyService: AddCompanyAsync\n" + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // UPDATE Company
        public async Task<ActionResult> UpdateCompanyAsync(UpdateCompanyViewModel model)
        {
            try
            {
                var company = await _repository.FindAsync(model.Id);

                if (company == null)
                    return new BadRequestObjectResult(NULL_RESULT);

                company.CompanyName = model.CompanyName;
                company.CompanyAddress = model.CompanyAddress;
                company.CompanyPhoneNumber = model.CompanyPhoneNumber;
                company.CompanyDescription = model.CompanyDescription;
                company.PhotoFileName = model.PhotoFileName;

                await _repository.UpdateAsync(company);
                return new OkObjectResult(JsonConvert.DeserializeObject<CompanyViewModel>
                    (JsonConvert.SerializeObject(company)));
            }
            catch (Exception ex)
            {
                _logger.LogError("CompanyService: UpdateCompanyAsync\n" + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // DELETE Company
        public async Task<ActionResult> DeleteCompanyAsync(int id)
        {
            try
            {
                var company = await _repository.FindAsync(id);

                if (company == null)
                    return new BadRequestObjectResult(NULL_RESULT);

                await _repository.RemoveAsync(company);
                return new OkObjectResult(JsonConvert.DeserializeObject<CompanyViewModel>
                    (JsonConvert.SerializeObject(company)));
            }
            catch (Exception ex)
            {
                _logger.LogError("CompanyService: DeleteCompanyAsync\n" + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        // ACTION SaveFiles
        public string SaveFile(HttpRequest request)
        {
            try
            {
                var httpRequest = request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/Company/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return filename;
            }
            catch (Exception ex)
            {
                _logger.LogError("CompanyService: SaveFile\n" + ex.Message);
                return "default_company_image.png";
            }
        }
    }
}
