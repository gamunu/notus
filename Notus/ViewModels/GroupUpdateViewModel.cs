using System;
using System.Collections.Generic;
using Notus.Model.Models;

namespace Notus.ViewModels
{
    public class GroupUpdateViewModel
    {
        public int GroupUpdateId { get; set; }

        public string Updatemsg { get; set; }

        public double? status { get; set; }

        public int GroupGoalId { get; set; }

        public DateTime UpdateDate { get; set; }

        public virtual GroupGoal GroupGoal { get; set; }

        public virtual ICollection<GroupComment> GroupComments { get; set; }

        public bool? IsSupported { get; set; }

        public int GroupUserId { get; set; }

        public string UserId { get; set; }

        public virtual ICollection<GroupUpdateSupport> GroupUpdateSupports { get; set; }
    }
}