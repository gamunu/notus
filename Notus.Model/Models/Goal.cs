using System;
using System.Collections.Generic;

namespace Notus.Model.Models
{
    public class Goal
    {
        public Goal()
        {
            CreatedDate = DateTime.Now;
            GoalStatusId = 1;
        }

        public int GoalId { get; set; }

        public string GoalName { set; get; }

        public string Desc { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double? Target { get; set; }

        public bool GoalType { get; set; }

        public int? MetricId { get; set; }

        public int GoalStatusId { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Metric Metric { get; set; }

        public virtual GoalStatus GoalStatus { get; set; }

        public virtual ICollection<Support> Supports { get; set; }

        public virtual ICollection<Update> Updates { get; set; }

        public virtual ICollection<SupportInvitation> SupportInvitations { get; set; }
    }
}