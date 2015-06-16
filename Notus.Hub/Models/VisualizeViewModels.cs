using System.ComponentModel.DataAnnotations;

namespace Notus.Hub.Models
{
    public class ChartControlViewModel
    {
        [Required]
        [Display(Name = "Cause of Disease or Injury")]
        public string CauseOf { get; set; }

        [Required]
        [Display(Name = "Metric")]
        public string Metric { get; set; }

        [Display(Name = "Risk Factor")]
        public string RiskFactor { get; set; }

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