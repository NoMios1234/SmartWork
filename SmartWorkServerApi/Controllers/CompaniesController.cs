using Microsoft.AspNetCore.Mvc;
using SmartWork.Core.Abstractions.Services;
using SmartWork.Core.Entities;
using SmartWork.Core.ViewModels.CompanyViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWorkServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET: api/Companies/GetCompanies
        [HttpGet("GetCompanies")]
        public Task<IEnumerable<CompanyViewModel>> GetCompanies()
        {
            return _companyService.GetCompaniesAsync(new SmartWork.Core.Specifications
                .Specification<Company>(c => c.Id != -1));
        }

        // POST: api/Companies/AddCompany
        [HttpPost("AddCompany")]
        public async Task<ActionResult> AddCompany(AddCompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                return await _companyService.AddCompanyAsync(model);
            }
            else
                return BadRequest(ModelState.Values.SelectMany(m => m.Errors));   
        }

        // PUT: api/Companies/UpdateCompany
        [HttpPut("UpdateCompany")]
        public async Task<ActionResult> UpdateCompany(UpdateCompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                return await _companyService.UpdateCompanyAsync(model);
            }
            else
                return BadRequest(ModelState.Values.SelectMany(m => m.Errors));
        }

        // DELETE: api/Companies/DeleteCompany/5
        [HttpDelete("DeleteCompany/{id}")]
        public async Task<ActionResult<Company>> Delete(int id)
        {
            return await _companyService.DeleteCompanyAsync(id);
        }

        // POST: api/Companies/SaveFile
        [HttpPost("SaveFile")]
        public JsonResult SaveFile()
        {
            return new JsonResult(_companyService.SaveFile(Request));
        }
    }
}