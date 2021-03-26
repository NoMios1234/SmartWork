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
    
    [ApiController]
    public class OfficesController : ControllerBase
    {
        ApplicationContext db;
        public OfficesController(ApplicationContext context)
        {
            db = context;
            if (!db.Office.Any())
            {
                db.Office.Add(new Office
                {
                    officeName = "DefalutOffice",
                    officeAddress = "",
                    isFavourite = true,
                    Subscribe = new Subscribe
                    {
                        name = "DefalutSubscribe",
                        price = 100,
                        desc = null
                    },
                    subscribeId = 1
                });
                db.SaveChanges();
            }
        }

        [Route("api/[controller]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Office>>> Get()
        {
            return await db.Office.ToListAsync();
        }

        // GET api/offices/5
        [Route("api/[controller]")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Office>> Get(int id)
        {
            Office office = await db.Office.FirstOrDefaultAsync(x => x.id == id);
            if (office == null)
                return NotFound();
            return new ObjectResult(office);
        }

        // POST api/offices
        [Route("api/[controller]")]
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
        [Route("api/[controller]")]
        [HttpPut]
        public async Task<ActionResult<Office>> Put(Office office)
        {
            if (office == null)
            {
                return BadRequest();
            }
            if (!db.Office.Any(x => x.id == office.id))
            {
                return NotFound();
            }

            db.Update(office);
            await db.SaveChangesAsync();
            return Ok(office);
        }

        // DELETE api/offices/5
        [Route("api/[controller]")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Office>> Delete(int id)
        {
            Office office = db.Office.FirstOrDefault(x => x.id == id);
            if (office == null)
            {
                return NotFound();
            }
            db.Office.Remove(office);
            await db.SaveChangesAsync();
            return Ok(office);
        }

        //сделать добавление комнат к оффису
        //сделать редактирование подписки

        // GET api/offices/ShowRooms/1
        [Route("api/[controller]/[action]/{id}")]
        public async Task<ActionResult<Office>> ShowRooms(int id)
        {
            Office office = await db.Office.FirstOrDefaultAsync(x => x.id == id);

            office.Rooms = db.Room.Where(r => r.officeId == id).Select
            (r => new RoomInfo
            {
                id = r.id,
                roomName = r.roomName,
                companyName = r.companyName,
                temperature = r.temperature,
                light = r.light,
                square = r.square,
                equipmentId = r.equipmentId,
                officeId = r.officeId
            }).ToList();
            return new ObjectResult(office);
           
        }

    }
}
