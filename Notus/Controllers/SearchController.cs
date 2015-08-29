using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Notus.Model.Models;
using Notus.Service;
using Notus.ViewModels;

namespace Notus.Controllers
{
    public class SearchController : Controller
    {
        private readonly IGoalService _goalService;
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;

        public SearchController(IGoalService goalService, IUserService userService, IGroupService groupService)
        {
            _goalService = goalService;
            _userService = userService;
            _groupService = groupService;
        }

        public ViewResult SearchAll(string searchText)
        {
            var searchViewModel = new SearchViewModel
            {
                Goals = Mapper.Map<IEnumerable<Goal>, IEnumerable<GoalViewModel>>(_goalService.SearchGoal(searchText)),
                Users = _userService.SearchUser(searchText),
                Groups =
                    Mapper.Map<IEnumerable<Group>, IEnumerable<GroupViewModel>>(_groupService.SearchGroup(searchText)),
                SearchText = searchText
            };
            ViewBag.searchtext = searchText;
            return View("SearchResult", searchViewModel);
        }
    }
}