using System.Collections.Generic;
using Notus.Model.Models;

namespace Notus.ViewModels
{
    public class GoalSupporterViewModel
    {
        public int GoalId { get; set; }

        public Goal Goal { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}