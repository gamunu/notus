using System;
using System.Collections.Generic;

namespace Notus.Model.Models
{
    public class Focus
    {
        public Focus()
        {
            CreatedDate = DateTime.Now;
        }

        public int FocusId { get; set; }

        public string FocusName { get; set; }

        public string Description { get; set; }

        public int GroupId { get; set; }

        public virtual Group Group { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<GroupGoal> GroupGoals { get; set; }
    }
}