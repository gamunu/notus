using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notus.Hub.Models
{
    public class BloodReport
    {
        public BloodReport()
        {
            Guid = System.Guid.NewGuid().ToString();
            Uploaded = DateTime.Now;
        }

        [Key]
        public string Guid { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public double Wbc { get; set; }
        public double Neut { get; set; }
        public double Lymph { get; set; }
        public double Mono { get; set; }
        public double Eo { get; set; }
        public double Baso { get; set; }
        public double Rbc { get; set; }
        public double Hgb { get; set; }
        public double Hct { get; set; }
        public double Mcv { get; set; }
        public double Mch { get; set; }
        public double Mchc { get; set; }
        public double Rdw { get; set; }
        public double Plt { get; set; }
        public DateTime ReportDate { get; set; }
        public DateTime Uploaded { get; set; }
    }
}