using System.Collections.Generic;
using Notus.Model.Models;

namespace Notus.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<GoalViewModel> Goals { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<GroupViewModel> Groups { get; set; }
        public string SearchText { get; set; }
    }
}