using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Notus.Hub.Models
{
    public class FitnessSettingsViewModel
    {
        [Display(Name = "Duration")]
        [Range(1, 1440)]
        public int Duration { get; set; }

        [Display(Name = "Steps")]
        [Range(1, 50000)]
        public int Steps { get; set; }

        [Display(Name = "Distance")]
        [Range(1, 160000)]
        public double Distance { get; set; }

        [Display(Name = "Calories burned")]
        [Range(1, 10000)]
        public double CaloriesBurned { get; set; }

        [Required]
        [Display(Name = "Height")]
        [Range(1, 500)]
        public double Height { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "Favorites")]
        public long FirstFitnessActivity { get; set; }

        [Required]
        [Display(Name = "Favorites")]
        public long SecondFitnessActivity { get; set; }

        [Required]
        [Display(Name = "Favorites")]
        public long ThirdFitnessActivity { get; set; }

        [Required]
        [Display(Name = "Favorites")]
        public long FourthFitnessActivity { get; set; }

    }

    public class AddActivityViewModel
    {
        [Required]
        [Display(Name = "Activity")]
        public long Activity { get; set; }
    }
}