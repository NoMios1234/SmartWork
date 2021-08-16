using Microsoft.AspNetCore.Mvc;
using SmartWork.Core.Abstractions.Services;
using SmartWork.Core.Entities;
using SmartWork.Core.Specifications;
using SmartWork.Core.ViewModels.OfficeViewModels;
using SmartWork.Core.ViewModels.SubscribeDetailViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWorkServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficesController : ControllerBase
    {
        private readonly IOfficeService _officeService;

        public OfficesController(IOfficeService officeService)
        {
            _officeService = officeService;
        }

        // GET: api/Offices/GetOffices
        [HttpGet("GetOffices")]
        public Task<IEnumerable<OfficeViewModel>> GetOffices()
        {
            return _officeService.GetOfficesAsync(new Specification<Office>(
                o => o.Id != -1));
        }

        // POST: api/Offices/GetOffice/5
        [HttpPost("GetOffice/{id}")]
        public Task<OfficeViewModel> GetOffice(int id)
        {
            return _officeService.FindOfficeAsync(id);
        }

        // POST: api/Offices/GetCompanyOffices/5
        [HttpPost("GetCompanyOffices/{id}")]
        public Task<IEnumerable<OfficeViewModel>> GetCompanyOffices(int id)
        {
            return _officeService.GetCompanyOfficesAsync(id);
        }       
        
        // POST: api/Offices/GetCompanyOffices/5
        [HttpPost("GetOfficeSubscribeDetails/{id}")]
        public Task<IEnumerable<SubscribeDetailViewModel>> GetOfficeSubscribeDetails(int id)
        {
            return _officeService.GetOfficeSubscribeDetailsAsync(id);
        }

        // POST: api/Offices/AddOffice
        [HttpPost("AddOffice")]
        public async Task<ActionResult> AddOffice(AddOfficeViewModel model)
        {
            if (ModelState.IsValid)
                return await _officeService.AddOfficeAsync(model);
            else
                return BadRequest(ModelState.Values.SelectMany(m => m.Errors));
        }

        // POST: api/Offices/SubscribeUser
        [HttpPost("SubscribeUser")]
        public Task<ActionResult> SubscribeUserToOffice(string userId, int subscribeDetailsId)
        {
            return _officeService.SubscribeUserToOfficeAsync(userId, subscribeDetailsId);
        }

        // PUT: api/Offices/AddOffice
        [HttpPut("UpdateOffice")]
        public async Task<ActionResult> UpdateOffice(UpdateOfficeViewModel model)
        {
            if (ModelState.IsValid)
                return await _officeService.UpdateOfficeAsync(model);
            else
                return BadRequest(ModelState.Values.SelectMany(m => m.Errors));
        }

        // DELETE: api/Offices/DeleteOffice
        [HttpDelete("DeleteOffice/{id}")]
        public Task<ActionResult> DeleteOfficeAsync(int id)
        {
            return _officeService.DeleteOfficeAsync(id);
        }

        // POST: api/Offices/SaveFile
        [HttpPost("SaveFile")]
        public JsonResult SaveFile()
        {
            return new JsonResult(_officeService.SaveFile(Request));
        }
    }
}
