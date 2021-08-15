using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWork.Core.Entities;
using SmartWork.Data.AppContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWork.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        ApplicationContext db;
        private readonly IWebHostEnvironment _env;
        public RoomsController(ApplicationContext context, IWebHostEnvironment env)
        {
            db = context;
            _env = env;

            if (!db.Room.Any())
            {
                db.Room.Add(new Room
                {
                    RoomName = "Big room",
                    RoomNumber = 1,
                    Temperature = 22,
                    Light = 1500,
                    Square = 150,
                    OfficeId = db.Office.FirstOrDefault(o => o.OfficeName == "Smart Work office").Id,
                    CompanyName = db.Company.FirstOrDefault(cp => cp.CompanyName == "SmartWork Company").CompanyName
                });
                db.SaveChanges();
            }
        }

        // GET api/rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> Get()
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
            return await db.Room.ToListAsync();
        }

        // GET api/rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> Get(int id)
        {
            List<Equipment> equipments = await db.Equipment.ToListAsync();
            foreach (var equipment in equipments)
            {
                equipment.MaterialEquipments = await db.MaterialEquipment.Where(eq => eq.EquipmentId == equipment.Id).ToListAsync();
                equipment.TechnicalEquipments = await db.TechnicalEquipment.Where(eq => eq.EquipmentId == equipment.Id).ToListAsync();
            }
            Room room = await db.Room.FirstOrDefaultAsync(x => x.Id == id);
            if (room == null)
                return NotFound();
            room.Equipments = equipments.Where(eq => eq.RoomId == room.Id).ToList();
            return new ObjectResult(room);
        }

        // POST api/rooms
        [HttpPost]
        public async Task<ActionResult<Room>> Post(Room room)
        {
            if (room == null)
            {
                return BadRequest();
            }
            db.Room.Add(room);
            await db.SaveChangesAsync();
            return Ok(room);
        }

        // PUT api/rooms/
        [HttpPut]
        public async Task<ActionResult<Room>> Put(Room room)
        {
            if (room == null)
            {
                return BadRequest();
            }
            if (!db.Room.Any(r => r.Id == room.Id))
            {
                return NotFound();
            }

            db.Update(room);
            await db.SaveChangesAsync();
            return Ok(room);
        }

        // DELETE api/rooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Room>> Delete(int id)
        {
            Room Room = db.Room.FirstOrDefault(r => r.Id == id);
            if (Room == null)
            {
                return NotFound();
            }
            db.Room.Remove(Room);
            await db.SaveChangesAsync();
            return Ok(Room);
        }

        [HttpGet("/Rooms/GetRoomEquipment/{id}")]
        public async Task<ActionResult<IEnumerable<Equipment>>> GetRoomEquipment(int id)
        {
            return await db.Equipment.Where(eq => eq.RoomId == id).ToListAsync();
        }
    }
}
