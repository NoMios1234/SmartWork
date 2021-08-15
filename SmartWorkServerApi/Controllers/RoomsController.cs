using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartWork.Core.Entities;
using SmartWork.Data.AppContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartWorkServerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ApplicationContext db;
        private readonly IWebHostEnvironment _env;
        public RoomsController(ApplicationContext context, IWebHostEnvironment env)
        {
            db = context;
            _env = env;
        }

        // GET api/rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> Get()
        {
            return await db.Room.ToListAsync();
        }

        // GET api/rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> Get(int id)
        {
            Room room = await db.Room.FirstOrDefaultAsync(x => x.Id == id);
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

        [HttpGet("GetEquipmentByRoomId/{id}")]
        public async Task<ActionResult<IEnumerable<Equipment>>> GetEquipmentByRoomId(int id)
        {
            return await db.Equipment.Where(eq => eq.RoomId == id).ToListAsync();
        }
    }
}
