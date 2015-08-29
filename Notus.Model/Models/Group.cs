using System;
using System.Collections.Generic;

namespace Notus.Model.Models
{
    public class Group
    {
        public Group()
        {
            CreatedDate = DateTime.Now;
        }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Focus> Foci { get; set; }

        public virtual ICollection<GroupInvitation> GroupInvitations { get; set; }

        public virtual ICollection<GroupRequest> GroupRequests { get; set; }
    }
}