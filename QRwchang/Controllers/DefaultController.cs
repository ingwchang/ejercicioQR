using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QRCoder;
using System.Drawing;
using System.IO;

namespace QRwchang.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult Generador()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Generador(string txtQRCode)
        {
            ViewBag.txtQRCode = txtQRCode;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(txtQRCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ViewBag.imageBytes = ms.ToArray();
                }
            }
            return View();
        }
    }
}