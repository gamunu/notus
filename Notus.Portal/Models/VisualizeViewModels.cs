using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Notus.Portal.Models
{
    public class ChartControlViewModel
    {
        [Required]
        [Display(Name = "Cause of Disease or Injury")]
        public string CauseOf;

        [Required]
        [Display(Name = "Metric")]
        public string Metric;

        [Required]
        [Display(Name = "Place")]
        public string Place;

        [Required]
        [Display(Name = "Year")]
        public string Year;

        [Required]
        [Display(Name = "Age")]
        public string Age;

        [Required]
        [Display(Name = "Depth")]
        public string Depth;

        [Required]
        [Display(Name = "Color")]
        public string Color;
    }
}