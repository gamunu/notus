using Notus.Model.Models;

namespace Notus.ViewModels
{
    public class GroupRequestViewModel
    {
        public int GroupId { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Group Group { get; set; }
    }
}