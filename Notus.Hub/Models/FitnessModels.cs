using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Notus.Hub.Models
{

    public class FitnessActivity
    {

        [Key]
        public long Id { get; set; }

        public string Name { get; set; }
    }

    public class HealthProfile
    {
        [Key]
        public long Id { get; set; }

        [ForeignKey("User")]
        public virtual string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public double Height { get; set; }

        public string Gender { get; set; }

        public int FitnessGloalDuration { get; set; }

        public int FitnessGloalSteps { get; set; }

        public double FitnessGloalDistance { get; set; }

        public double FitnessGloalCaloriesBurned { get; set; }
    }

    public class UserWeight
    {
        public UserWeight()
        {
            LoggedDate = DateTime.UtcNow;
        }

        [Key]
        public long Id { get; set; }
        public virtual ApplicationUser User { get; set; }
        public double Weight { get; set; }
        public DateTime LoggedDate { get; set; }
    }

    public class UserFitnessActivity
    {
        public UserFitnessActivity()
        {
            StartTime = DateTime.UtcNow;
        }

        [Key]
        public long Id { get; set; }
        [ForeignKey("Activity")]
        public virtual long ActivityId { get; set; }
        public virtual FitnessActivity Activity { get; set; }
        public int Steps { get; set; }
        public double Distance { get; set; }
        public double Energy { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [ForeignKey("User")]
        public virtual string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}