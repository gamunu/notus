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
        public string CauseOf { get; set; }

        [Required]
        [Display(Name = "Metric")]
        public string Metric { get; set; }

        [Required]
        [Display(Name = "Place")]
        public string Place { get; set; }

        [Required]
        [Display(Name = "Year")]
        public string Year { get; set; }

        [Required]
        [Display(Name = "Age")]
        public string Age { get; set; }

        [Required]
        [Display(Name = "Depth")]
        public string Depth { get; set; }

        [Required]
        [Display(Name = "Color")]
        public string Color { get; set; }
    }
}