using System.Collections.Generic;
using Notus.Model.Models;

namespace Notus.ViewModels
{
    public class GroupMemberViewModel
    {
        public int GroupId { get; set; }

        public Group Group { get; set; }

        public int? GroupUserId { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}