using System.Collections.Generic;
using Notus.Model.Models;

namespace Notus.ViewModels
{
    public class GroupUpdateListViewModel
    {
        public IEnumerable<GroupUpdateViewModel> GroupUpdates { get; set; }

        public double? Target { get; set; }

        public Metric Metric { get; set; }
    }
}