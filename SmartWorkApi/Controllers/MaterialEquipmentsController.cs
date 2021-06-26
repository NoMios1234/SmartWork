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
    public class MaterialEquipmentsController : ControllerBase
    {
        private readonly ApplicationContext db;

        public MaterialEquipmentsController(ApplicationContext context)
        {
            db = context;
        }
        // GET api/materialequipments
        [HttpGet]
        public async Task<ActionResult<ICollection<MaterialEquipment>>> GetMaterialEquipment()
        {
            return await db.MaterialEquipment.ToListAsync();
        }

        // GET api/materialequipments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialEquipment>> GetMaterialEquipmentById(int id)
        {
            return await db.MaterialEquipment.FirstOrDefaultAsync(t => t.Id == id);
        }

        // POST api/materialequipments
        [HttpPost]
        public async Task<ActionResult<MaterialEquipment>> AddMaterialEquipment(MaterialEquipment equipment)
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
        public async Task<ActionResult<MaterialEquipment>> UpdateMaterialEquipment(MaterialEquipment equipment)
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
        public async Task<ActionResult<MaterialEquipment>> DeleteMaterialEquipment(int id)
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
