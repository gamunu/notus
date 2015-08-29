using System.Collections.Generic;

namespace Notus.Model.Models
{
    public class Metric
    {
        public int MetricId { get; set; }

        public string Type { get; set; }

        public virtual ICollection<Goal> Goals { get; set; }

        public virtual ICollection<GroupGoal> GroupGoals { get; set; }
    }
}