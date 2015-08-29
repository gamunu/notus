using Notus.Service;
using PagedList;

namespace Notus.ViewModels
{
    public class GroupsPageViewModel
    {
        public IPagedList<GroupsItemViewModel> GroupList { get; set; }

        public GroupFilter Filter { get; set; }
    }
}