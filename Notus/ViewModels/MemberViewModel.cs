﻿using Notus.Model.Models;

namespace Notus.ViewModels
{
    public class MemberViewModel
    {
        public int GroupId { get; set; }

        public Group Group { get; set; }

        public string UserId { get; set; }

        public int GroupUserId { get; set; }

        public string UserName { get; set; }

        public virtual ApplicationUser User { get; set; }

        //public IEnumerable<User> Users { get; set; }

        //public IEnumerable<GroupUser> GroupUser { get; set; }
    }
}