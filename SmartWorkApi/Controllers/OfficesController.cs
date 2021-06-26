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
    public class OfficesController : ControllerBase
    {
        private readonly ApplicationContext db;
        private readonly IWebHostEnvironment _env;
        public OfficesController(ApplicationContext context, IWebHostEnvironment env)
        {
            db = context;
            _env = env;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Office>>> Get()
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
            foreach(var office in offices)
            {
                office.Rooms = await db.Room.Where(r => r.OfficeId == office.Id).ToListAsync();
            }
            return await db.Office.ToListAsync();
        }

        // GET api/offices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Office>> Get(int id)
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
            Office office = await db.Office.FirstOrDefaultAsync(o => o.Id == id);
            office.Rooms = await db.Room.Where(r => r.OfficeId == office.Id).ToListAsync();
            return new ObjectResult(office);
        }

        // POST api/offices
        [HttpPost]
        public async Task<ActionResult<Office>> Post(Office office)
        {
            if (office == null)
            {
                return BadRequest();
            }

            db.Office.Add(office);
            await db.SaveChangesAsync();
            return Ok(office);
        }

        // PUT api/offices/
        [HttpPut]
        public async Task<ActionResult<Office>> Put(Office office)
        {
            if (office == null)
            {
                return BadRequest();
            }
            if (!db.Office.Any(o => o.Id == office.Id))
            {
                return NotFound();
            }

            db.Update(office);
            await db.SaveChangesAsync();
            return Ok(office);
        }

        // DELETE api/offices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Office>> Delete(int id)
        {
            Office Office = db.Office.FirstOrDefault(o => o.Id == id);
            if (Office == null)
            {
                return NotFound();
            }
            db.Office.Remove(Office);
            await db.SaveChangesAsync();
            return Ok(Office);
        }

        [HttpGet("GetRoomsByOfficeId/{id}")]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomsByOfficeId(int id)
        {
            return await db.Room.Where(r => r.OfficeId == id).ToListAsync();
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
                var physicalPath = _env.ContentRootPath + "/Photos/Office/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("default_office_image.png");
            }
        }
    }
}
