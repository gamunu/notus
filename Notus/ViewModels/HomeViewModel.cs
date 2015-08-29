using System.Collections.Generic;

namespace Notus.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<NotificationsViewModel> Notification { get; set; }
        public int? Count { get; set; }
    }
}