using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Notus.Model.Models
{
    public class GoalUpdate
    {
        public GoalUpdate()
        {
            DateOfUpdate = DateTime.Now;
        }

        [ScaffoldColumn(false)]
        public int UpdateId { get; set; }

        public string Updatemsg { get; set; }

        public double? status { get; set; }

        public int GoalId { get; set; }

        public DateTime DateOfUpdate { get; set; }

        public virtual Goal Goal { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}