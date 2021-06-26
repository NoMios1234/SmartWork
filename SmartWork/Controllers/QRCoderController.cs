using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using SmartWork.Core.Models;
using SmartWork.Data.Data;
using System;
using System.Drawing;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartWork.Controllers
{
    public class QRCoderController : Controller
    {
        private readonly UserManager<User> _userManager;
        public QRCoderController(ApplicationContext context, UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> IndexAsync()
        {
            User user = await _userManager.FindByIdAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user != null)
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(user.ToString(),
                QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);
                return View(BitmapToBytes(qrCodeImage));
            }
            return
                RedirectToAction("Login");
        }
      
        private static Byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }   
}
