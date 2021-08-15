using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWork.Core.Entities;
using SmartWork.Data.AppContext;
using SmartWork.ViewModels.CompanyViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        ApplicationContext db;
        private readonly IWebHostEnvironment _env;

        public CompaniesController(ApplicationContext context, IWebHostEnvironment env)
        {
            db = context;
            _env = env;

            if (!db.Company.Any())
            {
                db.Company.Add(new Company
                {
                    CompanyName = "SmartWork Company",
                    CompanyAddress = "Smart Work street",
                    CompanyPhoneNumber = "+280441257896",  
                });
                db.SaveChanges();
            }
        }
        // GET api/companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> Get()
        {
            List<Equipment> equipments = await db.Equipment.ToListAsync();
            foreach (var equipment in equipments)
            {
                equipment.MaterialEquipments = await db.MaterialEquipment.Where(eq => eq.EquipmentId == equipment.Id).ToListAsync();
                equipment.TechnicalEquipments = await db.TechnicalEquipment.Where(eq => eq.EquipmentId == equipment.Id).ToListAsync();
            }
            List<Room> rooms = await db.Room.ToListAsync();
            foreach (var room in rooms)
            {
                room.Equipments = equipments.Where(eq => eq.RoomId == room.Id).ToList();
            }
            List<Office> offices = await db.Office.ToListAsync();
            foreach (var office in offices)
            {
                office.Rooms = await db.Room.Where(r => r.OfficeId == office.Id).ToListAsync();
            }
            List<Company> companies = await db.Company.ToListAsync();
            foreach (var company in companies)
            {
                company.Offices = await db.Office.Where(o => o.CompanyId == company.Id).ToListAsync();
            }
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

        [HttpGet("/Companies/GetCompanyOffices/{id}")]
        public async Task<ActionResult<IEnumerable<Office>>> GetCompanyOffices(int id)
        {
            return await db.Office.Where(o => o.CompanyId == id).ToListAsync();
        }
    }
}
