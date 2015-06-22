using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Notus.Hub.Models;
using Tesseract;

namespace Notus.Hub.Controllers
{
    [Authorize]
    public class InfoController : Controller
    {
        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();
        private readonly string _pattern = @"(WBC|NEUT|LYMPH|MONO|EO|BASO|RBC|HGB|HCT|MCV|EOMCH|MCH|MCHC|RDW-CV|PLT)(?:\s)(\d+\.\d+)";
        private readonly string _datepattern = @"(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})";

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
        public async Task<ActionResult> Upload(BloodReportViewModel model)
        {

            if (Request.Form["upload"] != null)
            {
                var imageTypes = new string[]
                {
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png",
                    "image/tiff"
                };

                if (model.ImageUpload == null || model.ImageUpload.ContentLength <= 0)
                {
                    ModelState.AddModelError("ImageUpload", "This field is required");

                }
                else if (!imageTypes.Contains(model.ImageUpload.ContentType))
                {
                    ModelState.AddModelError("ImageUpload", "Please choose either a GIF, JPG or PNG image.");
                }
                else
                {
                    var uploadDir = "~/Uploads";
                    var imagePath = Path.Combine(Server.MapPath(uploadDir), model.ImageUpload.FileName);
                    var imageUrl = Path.Combine(uploadDir, model.ImageUpload.FileName);
                    model.ImageUpload.SaveAs(imagePath);

                    using (
                        var engine = new TesseractEngine(Server.MapPath(@"~/tessdata"), "eng", EngineMode.TesseractOnly)
                        )
                    {
                        using (var image = new System.Drawing.Bitmap(model.ImageUpload.InputStream))
                        {
                            using (var pix = PixConverter.ToPix(image))
                            {
                                using (var page = engine.Process(pix))
                                {
                                    string text = page.GetText();
                                    BloodReportViewModel report = ParseBloodReport(text);
                                    report.ReportDate = GetReportDate(text);

                                    return View(report);
                                }
                            }
                        }
                    }
                }
            }
            else if (Request.Form["confirm"] != null)
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                var bloodreport = new BloodReport
                {
                    UserId = User.Identity.GetUserId(),
                    Baso = model.Baso,
                    Eo = model.Eo,
                    Hct = model.Hct,
                    Hgb = model.Hgb,
                    Lymph = model.Lymph,
                    Mch = model.Mch,
                    Mchc = model.Mchc,
                    Mcv = model.Mcv,
                    Mono = model.Mono,
                    Neut = model.Neut,
                    Plt = model.Plt,
                    Rbc = model.Rbc,
                    Rdw = model.Rdw,
                    Wbc = model.Wbc,
                    ReportDate = model.ReportDate
                };

                _dbContext.BloodReports.Add(bloodreport);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index", "Reports");
            }
            return View();
        }

        public BloodReportViewModel ParseBloodReport(string text)
        {

            Regex regex = new Regex(_pattern, RegexOptions.IgnoreCase);

            MatchCollection matches = regex.Matches(text);

            BloodReportViewModel report = new BloodReportViewModel();

            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    if (match.Groups.Count < 2) continue;

                    string name = match.Groups[1].Value;
                    double value;

                    if (!double.TryParse(match.Groups[2].Value, out value))
                    {
                        value = double.MinValue;
                    }

                    switch (name.ToLower())
                    {
                        case "wbc":
                            report.Wbc = value;
                            break;
                        case "neut":
                            report.Neut = value;
                            break;
                        case "lymph":
                            report.Lymph = value;
                            break;
                        case "mono":
                            report.Mono = value;
                            break;
                        case "eo":
                            report.Eo = value;
                            break;
                        case "baso":
                            report.Baso = value;
                            break;
                        case "rbc":
                            report.Rbc = value;
                            break;
                        case "hgb":
                            report.Hgb = value;
                            break;
                        case "hct":
                            report.Hct = value;
                            break;
                        case "mcv":
                            report.Mcv = value;
                            break;
                        case "mch":
                            report.Mch = value;
                            break;
                        case "mchc":
                            report.Mchc = value;
                            break;
                        case "rdw-cv":
                            report.Rdw = value;
                            break;
                        case "plt":
                            report.Plt = value;
                            break;
                    }
                }
            }
            return report;
        }

        public DateTime GetReportDate(string text)
        {
            Regex regex = new Regex(_datepattern, RegexOptions.IgnoreCase);

            Match match = regex.Match(text);

            if (!match.Success) return new DateTime();

            DateTime reportDate;
            if (DateTime.TryParseExact(match.Value, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out reportDate))
                return reportDate;

            return new DateTime();
        }
    }
}
