using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWork.Core.Models;
using SmartWork.Data.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWorkServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        private readonly ApplicationContext db;

        public EquipmentsController(ApplicationContext context)
        {
            db = context;
        }

        // GET api/equipments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipment>>> Get()
        {
            List<Equipment> equipments = await db.Equipment.ToListAsync();
            foreach (var equipment in equipments)
            {
                equipment.MaterialEquipments = await db.MaterialEquipment.Where(eq => eq.EquipmentId == equipment.Id).ToListAsync();
                equipment.TechnicalEquipments = await db.TechnicalEquipment.Where(eq => eq.EquipmentId == equipment.Id).ToListAsync();
            }
            return await db.Equipment.ToArrayAsync();
        }

        // GET api/equipments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Equipment>> Get(int id)
        {
            if(!db.Equipment.Where(eq => eq.Id == id).Any())
            {
                return BadRequest();
            }
            Equipment equipment = await db.Equipment.FirstOrDefaultAsync(eq => eq.Id == id);
            equipment.MaterialEquipments = await db.MaterialEquipment.Where(eq => eq.EquipmentId == equipment.Id).ToListAsync();
            equipment.TechnicalEquipments = await db.TechnicalEquipment.Where(eq => eq.EquipmentId == equipment.Id).ToListAsync();
            return new ObjectResult(equipment);
        }

        // POST api/equipments
        [HttpPost]
        public async Task<ActionResult<Equipment>> Post(Equipment equipment)
        {
            if (equipment == null)
            {
                return BadRequest();
            }

            db.Equipment.Add(equipment);
            await db.SaveChangesAsync();
            return Ok(equipment);
        }

        // PUT api/equipments/
        [HttpPut]
        public async Task<ActionResult<Equipment>> Put(Equipment equipment)
        {
            if (equipment == null)
            {
                return BadRequest();
            }
            if (!db.Equipment.Any(eq => eq.Id == equipment.Id))
            {
                return NotFound();
            }

            db.Update(equipment);
            await db.SaveChangesAsync();
            return Ok(equipment);
        }

        // DELETE api/equipments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Equipment>> Delete(int id)
        {
            Equipment Equipment = db.Equipment.FirstOrDefault(eq => eq.Id == id);
            if (Equipment == null)
            {
                return NotFound();
            }
            db.Equipment.Remove(Equipment);
            await db.SaveChangesAsync();
            return Ok(Equipment);
        }

        [HttpGet("GetTechnicalEquipment/{id}")]
        public async Task<ActionResult<IEnumerable<TechnicalEquipment>>> GetTechnicalEquipment(int id)
        {
            return await db.TechnicalEquipment.Where(eq => eq.EquipmentId == id).ToListAsync();
        }

        [HttpGet("GetMaterialEquipment/{id}")]
        public async Task<ActionResult<IEnumerable<MaterialEquipment>>> GetMaterialEquipment(int id)
        {
            return await db.MaterialEquipment.Where(eq => eq.EquipmentId == id).ToListAsync();
        }
    }
}
