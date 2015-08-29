using Notus.Model.Models;

namespace Notus.ViewModels
{
    public class FollowRequestViewModel
    {
        public string FromUserId { get; set; }

        public string ToUserId { get; set; }

        public virtual ApplicationUser FromUser { get; set; }

        public virtual ApplicationUser ToUser { get; set; }
    }
}