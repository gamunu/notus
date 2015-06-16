using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notus.Hub.Models;
using Tesseract;

namespace Notus.Hub.Controllers
{
    [Authorize]
    public class InfoController : Controller
    {

        private static string _Confidance;
        private static string _Results;

        // GET: Info
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Upload()
        {
            return View();
        }

        // POST: /Info/Upload
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(BloodTestViewModel model)
        {
            var imageTypes = new string[]
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (model.ImageUpload == null || model.ImageUpload.ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "This field is required");

            }
            else if (!imageTypes.Contains(model.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                {
                    var uploadDir = "~/Uploads";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, model.ImageUpload.FileName);
                    model.ImageUpload.SaveAs(imagePath);

                    using (var engine = new TesseractEngine(Server.MapPath(@"~/tessdata"), "eng", EngineMode.Default))
                    {
                        using (var image = new System.Drawing.Bitmap(model.ImageUpload.InputStream))
                        {
                            using (var pix = PixConverter.ToPix(image))
                            {
                                using (var page = engine.Process(pix))
                                {
                                    _Confidance = string.Format("{0:P}", page.GetMeanConfidence());
                                    _Results = page.GetText();

                                    return RedirectToAction("Result");
                                }
                            }
                        }
                    }
                }
            }
            return View();
        }

        public ActionResult Result()
        {
            ViewBag.Confidance = _Confidance;
            ViewBag.Results = _Results;
            return View();
        }
    }
}
