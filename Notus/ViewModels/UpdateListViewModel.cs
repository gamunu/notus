using System.Collections.Generic;
using Notus.Model.Models;

namespace Notus.ViewModels
{
    public class UpdateListViewModel
    {
        public IEnumerable<UpdateViewModel> Updates { get; set; }

        public double? Target { get; set; }

        public Metric Metric { get; set; }
    }
}