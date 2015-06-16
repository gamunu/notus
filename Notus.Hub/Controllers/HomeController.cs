using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Notus.Hub.Models;

namespace Notus.Hub.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();
        private readonly List<SelectListItem> _genders = new List<SelectListItem>
            { 
                new SelectListItem {Text = "Male", Value = "Male"},
                new SelectListItem {Text = "Female", Value = "Female"},
                new SelectListItem {Text = "Other", Value = "Other"},
                new SelectListItem {Text = "Decline to state", Value = "Decline to state"}
            };


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }

        // GET: /Account/Register
        public async Task<ViewResult> Settings()
        {
            var user = User.Identity.GetUserId();
            var healthProfile = await _dbContext.HealthProfiles.FirstOrDefaultAsync(x => x.User.Id == user);

            ViewBag.Gender = _genders;

            if (healthProfile == null)
                return View();


            return View(new FitnessSettingsViewModel
            {
                CaloriesBurned = healthProfile.FitnessGloalCaloriesBurned,
                Distance = healthProfile.FitnessGloalDistance,
                Duration = healthProfile.FitnessGloalDuration,
                Steps = healthProfile.FitnessGloalSteps,
                Height = healthProfile.Height,
                Gender = healthProfile.Gender
            });
        }

        // POST: /Home/Settings
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ViewResult> Settings(FitnessSettingsViewModel model)
        {
            ViewBag.Gender = _genders;

            if (ModelState.IsValid)
            {
                var user = User.Identity.GetUserId();

                var healthProfile = await _dbContext.HealthProfiles.FirstOrDefaultAsync(x => x.User.Id == user);

                if (healthProfile == null)
                {
                    healthProfile = new HealthProfile();
                    _dbContext.Entry(healthProfile).State = EntityState.Added;
                    healthProfile.UserId = user;
                }
                else
                {
                    _dbContext.Entry(healthProfile).State = EntityState.Modified;
                }

                healthProfile.Gender = model.Gender;
                healthProfile.Height = model.Height;

                //Goals
                healthProfile.FitnessGloalCaloriesBurned = model.CaloriesBurned;
                healthProfile.FitnessGloalDistance = model.Distance;
                healthProfile.FitnessGloalSteps = model.Steps;
                healthProfile.FitnessGloalDuration = model.Duration;

                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}