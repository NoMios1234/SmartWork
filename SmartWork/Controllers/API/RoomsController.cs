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
            if(!db.Room.Any())
            {
                db.Room.Add(new Room
                {
                    roomName = "firstRoom",
                    roomNumber = 1,
                    companyName = null,
                    temperature = 23,
                    light = 1500,
                    square = 150,
                    equipmentId = 1,
                    Equipment = new Equipment
                    {
                        equipmentDesc = "firstDesc",
                        technicalEquipment = new List<TechnicalEquipment>
                        {
                            new TechnicalEquipment
                            {
                                equipmentName = "firsttech",
                                equipmentCount = 150,
                                equipmentDesc = "rterfgdg",
                                available = true,
                                roomId = 1
                            },
                            new TechnicalEquipment
                            {
                                equipmentName = "secondtech",
                                equipmentCount = 250,
                                equipmentDesc = "rterfgdg",
                                available = false,
                                roomId = 1
                            }
                        }
                    }
                });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> Get()
        {
            return await db.Room.ToListAsync();
        }

        // GET api/rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> Get(int id)
        {
            Room room = await db.Room.FirstOrDefaultAsync(x => x.id == id);
            if (room == null)
                return NotFound();
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
            if (!db.Room.Any(x => x.id == room.id))
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
            Room room = db.Room.FirstOrDefault(x => x.id == id);
            if (room == null)
            {
                return NotFound();
            }
            db.Room.Remove(room);
            await db.SaveChangesAsync();
            return Ok(room);
        }
    }
}
