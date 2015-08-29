using System.Collections.Generic;
using Notus.Model.Models;

namespace Notus.ViewModels
{
    public class UpdateSupportersViewModel
    {
        public int UpdateId { get; set; }

        public Update Update { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}