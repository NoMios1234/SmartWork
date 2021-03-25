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
    public class OfficesController : ControllerBase
    {
        ApplicationContext db;
        public OfficesController (ApplicationContext context)
        {
            db = context;

            if(!db.Office.Any())
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
                });;
                db.SaveChanges();
            }  
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Office>>> Get()
        {
            return await db.Office.ToListAsync();
        }

        //сделать добавление комнат к оффису
        //сделать редактирование подписки

    }
}
