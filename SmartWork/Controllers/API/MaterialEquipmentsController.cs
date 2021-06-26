using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWork.Core.Models;
using SmartWork.Data.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialEquipmentsController : ControllerBase
    {
        ApplicationContext db;

        public MaterialEquipmentsController(ApplicationContext context)
        {
            db = context;
        }
        // GET api/materialequipments
        [HttpGet]
        public async Task<ActionResult<ICollection<MaterialEquipment>>> Get()
        {
            return await db.MaterialEquipment.ToListAsync();
        }

        // GET api/materialequipments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialEquipment>> Get(int id)
        {
            return await db.MaterialEquipment.FirstOrDefaultAsync(t => t.Id == id);
        }
        // GET api/materialequipments
        public async Task<ActionResult<MaterialEquipment>> Post(MaterialEquipment equipment)
        {
            if (equipment == null)
            {
                return BadRequest();
            }
            db.MaterialEquipment.Add(equipment);
            await db.SaveChangesAsync();
            return Ok(equipment);
        }

        // PUT api/materialequipments/
        [HttpPut]
        public async Task<ActionResult<MaterialEquipment>> Put(MaterialEquipment equipment)
        {
            if (equipment == null)
            {
                return BadRequest();
            }
            if (!db.MaterialEquipment.Any(t => t.Id == equipment.Id))
            {
                return NotFound();
            }

            db.Update(equipment);
            await db.SaveChangesAsync();
            return Ok(equipment);
        }

        // DELETE api/materialequipments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MaterialEquipment>> Delete(int id)
        {
            MaterialEquipment MaterialEquipment = db.MaterialEquipment.FirstOrDefault(eq => eq.Id == id);
            if (MaterialEquipment == null)
            {
                return NotFound();
            }
            db.MaterialEquipment.Remove(MaterialEquipment);
            await db.SaveChangesAsync();
            return Ok(MaterialEquipment);
        }
    }
}
