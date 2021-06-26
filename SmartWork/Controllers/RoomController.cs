using Microsoft.AspNetCore.Mvc;

namespace SmartWork.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
