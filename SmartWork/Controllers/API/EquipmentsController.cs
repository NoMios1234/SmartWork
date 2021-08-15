using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWork.Core.Entities;
using SmartWork.Core.Entities;
using SmartWork.Data.AppContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentsController : ControllerBase
    {
        ApplicationContext db;

        public EquipmentsController(ApplicationContext context)
        {
            db = context;

            if (!db.Equipment.Any())
            {
                db.Equipment.Add(new Equipment
                {
                    EquipmentDesc = "defaultDesc",
                    RoomId = db.Room.FirstOrDefault().Id               
                });
                db.SaveChanges();
            }

            if (!db.TechnicalEquipment.Any())
            {
                db.TechnicalEquipment.Add(new TechnicalEquipment
                {
                    EquipmentName = "firsttech",
                    EquipmentCount = 150,
                    EquipmentDesc = "rterfgdg",
                    Available = true,
                    EquipmentId = db.Equipment.FirstOrDefault().Id

                });
                db.TechnicalEquipment.Add(new TechnicalEquipment
                {
                    EquipmentName = "secondtech",
                    EquipmentCount = 250,
                    EquipmentDesc = "rterfgdg",
                    Available = false,
                    EquipmentId = db.Equipment.FirstOrDefault().Id
                });
                db.SaveChanges();
            }
            if (!db.MaterialEquipment.Any())
            {
                db.MaterialEquipment.Add(new MaterialEquipment
                {
                    EquipmentName = "firstmaterial",
                    EquipmentCount = 75,
                    EquipmentDesc = "rterfgdg",
                    Available = true,
                    EquipmentId = db.Equipment.FirstOrDefault().Id
                });
                db.MaterialEquipment.Add(new MaterialEquipment
                {
                    EquipmentName = "secondmaterial",
                    EquipmentCount = 42,
                    EquipmentDesc = "rterfgdg",
                    Available = false,
                    EquipmentId = db.Equipment.FirstOrDefault().Id
                });
                db.SaveChanges();
            }
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

        [HttpGet("/Equipments/GetTechnicalEquipment/{id}")]
        public async Task<ActionResult<IEnumerable<TechnicalEquipment>>> GetTechnicalEquipment(int id)
        {
            return await db.TechnicalEquipment.Where(eq => eq.EquipmentId == id).ToListAsync();
        }

        [HttpGet("/Equipments/GetMaterialEquipment/{id}")]
        public async Task<ActionResult<IEnumerable<MaterialEquipment>>> GetMaterialEquipment(int id)
        {
            return await db.MaterialEquipment.Where(eq => eq.EquipmentId == id).ToListAsync();
        }
    }
}
