using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWork.Models;
using System;
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
        public RoomsController(ApplicationContext context)
        {
            db = context;
            if (!db.Room.Any())
            {
                db.Room.Add(new Room
                {
                    RoomName = "firstRoom",
                    RoomNumber = 1,
                    CompanyName = null,
                    Temperature = 23,
                    Light = 1500,
                    Square = 150,
                    OfficeId = db.Office.FirstOrDefault().Id
                });
                db.SaveChanges();
            }
        }

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

        // GET api/Rooms/5
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

        // POST api/Rooms
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

        // PUT api/Rooms/
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

        // DELETE api/Rooms/5
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
    }
}
