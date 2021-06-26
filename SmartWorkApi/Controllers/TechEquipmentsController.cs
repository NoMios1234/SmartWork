using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWork.Core.Models;
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
    public class TechEquipmentsController : ControllerBase
    {
        private readonly ApplicationContext db;

        public TechEquipmentsController(ApplicationContext context)
        {
            db = context;
        }
        // GET api/techequipments
        [HttpGet]
        public async Task<ActionResult<ICollection<TechnicalEquipment>>> Get()
        {
            return await db.TechnicalEquipment.ToListAsync();
        }

        // GET api/techequipments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TechnicalEquipment>> Get(int id)
        {
            return await db.TechnicalEquipment.FirstOrDefaultAsync(t => t.Id == id);
        }

        // POST api/techequipments
        [HttpPost]
        public async Task<ActionResult<TechnicalEquipment>> Post(TechnicalEquipment equipment)
        {
            if(equipment == null)
            {
                return BadRequest();
            }
            db.TechnicalEquipment.Add(equipment);
            await db.SaveChangesAsync();
            return Ok(equipment);
        }

        // PUT api/techequipments/
        [HttpPut]
        public async Task<ActionResult<TechnicalEquipment>> Put(TechnicalEquipment equipment)
        {
            if (equipment == null)
            {
                return BadRequest();
            }
            if (!db.TechnicalEquipment.Any(t => t.Id == equipment.Id))
            {
                return NotFound();
            }

            db.Update(equipment);
            await db.SaveChangesAsync();
            return Ok(equipment);
        }

        // DELETE api/techequipments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TechnicalEquipment>> Delete(int id)
        {
            TechnicalEquipment TechEquipment = db.TechnicalEquipment.FirstOrDefault(eq => eq.Id == id);
            if (TechEquipment == null)
            {
                return NotFound();
            }
            db.TechnicalEquipment.Remove(TechEquipment);
            await db.SaveChangesAsync();
            return Ok(TechEquipment);
        }
    }
}
