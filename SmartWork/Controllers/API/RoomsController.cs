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
            List<Room> rooms = await db.Room.ToListAsync();
            foreach (var room in rooms)
            {
                room.Equipments = await db.Equipment.Where(eq => eq.RoomId == room.Id).ToListAsync();
            }
            return await db.Room.ToListAsync();
        }

        // GET api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> Get(int id)
        {
            Room room = await db.Room.FirstOrDefaultAsync(x => x.Id == id);
            if (room == null)
                return NotFound();
            room.Equipments = await db.Equipment.Where(eq => eq.RoomId == room.Id).ToListAsync();
            return new ObjectResult(room);
        }

        // POST api/Rooms
        [HttpPost]
        public async Task<ActionResult<Room>> Post(Room Room)
        {
            if (Room == null)
            {
                return BadRequest();
            }
            db.Room.Add(Room);
            await db.SaveChangesAsync();
            return Ok(Room);
        }

        // PUT api/Rooms/
        [HttpPut]
        public async Task<ActionResult<Room>> Put(Room Room)
        {
            if (Room == null)
            {
                return BadRequest();
            }
            if (!db.Room.Any(x => x.Id == Room.Id))
            {
                return NotFound();
            }

            db.Update(Room);
            await db.SaveChangesAsync();
            return Ok(Room);
        }

        // DELETE api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Room>> Delete(int id)
        {
            Room Room = db.Room.FirstOrDefault(x => x.Id == id);
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
