using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

using QRCoder;
using SmartWork.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace SmartWork.Controllers
{
    public class QRCoderController : Controller
    {
        ApplicationContext db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public QRCoderController(ApplicationContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = context;
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


        //[HttpPost]
        //public IActionResult Index(string qrText)
        //{
        //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //    QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText,
        //    QRCodeGenerator.ECCLevel.Q);
        //    QRCode qrCode = new QRCode(qrCodeData);
        //    Bitmap qrCodeImage = qrCode.GetGraphic(20);
        //    return View(BitmapToBytes(qrCodeImage));
        //}
       
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
