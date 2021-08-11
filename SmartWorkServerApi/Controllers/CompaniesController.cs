using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWork.Core.Entities;
using SmartWork.Core.ViewModels;
using SmartWork.Data.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWorkServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ApplicationContext db;
        private readonly IWebHostEnvironment _env;

        public CompaniesController(ApplicationContext context, IWebHostEnvironment env)
        {
            db = context;
            _env = env;
        }
        // GET api/companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> Get()
        {
 
            return await db.Company.ToListAsync();
        }

        // POST api/companies
        [HttpPost]
        public async Task<ActionResult<Company>> Post(AddCompanyViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                Company company = new Company
                {
                    CompanyName = model.CompanyName,
                    CompanyAddress = model.CompanyAddress,
                    CompanyDescription = model.CompanyDescription,
                    CompanyPhoneNumber = model.CompanyPhoneNumber,
                    PhotoFileName = model.PhotoFileName
                };
                db.Company.Add(company);
                await db.SaveChangesAsync();
                return Ok(company);
            }
            else
                return BadRequest();   
        }

        // PUT api/companies/
        [HttpPut]
        public async Task<ActionResult<Company>> Put(Company company)
        {
            if (company == null)
            {
                return BadRequest();
            }
            if (!db.Company.Any(o => o.Id == company.Id))
            {
                return NotFound();
            }

            db.Update(company);
            await db.SaveChangesAsync();
            return Ok(company);
        }

        // DELETE api/companies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Company>> Delete(int id)
        {
            Company Company = db.Company.FirstOrDefault(r => r.Id == id);
            if (Company == null)
            {
                return NotFound();
            }
            db.Company.Remove(Company);
            await db.SaveChangesAsync();
            return Ok(Company);
        }

        [HttpGet("GetOfficesByCompanyId/{id}")]
        public async Task<ActionResult<IEnumerable<Office>>> GetOfficesByCompanyId(int id)
        {
            return await db.Office.Where(o => o.CompanyId == id).ToListAsync();
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {            
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/Company/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("default_company_image.png");
            }
        }
    }
}