﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace Notus.ViewModels
{
    public class GoalsPageViewModel
    {
        public GoalsPageViewModel(string selectedFilter, string selectedSort)
        {
            FilterBy = new SelectList(new[]
            {
                new SelectListItem {Text = "All", Value = "All"},
                new SelectListItem {Text = "My Goals", Value = "My Goals"},
                new SelectListItem {Text = "My Followed Goals", Value = "My Followed Goals"},
                new SelectListItem {Text = "My Followings Goals", Value = "My Followings Goals"}
            }, "Text", "Value", selectedFilter);
            SortBy = new SelectList(new[]
            {
                new SelectListItem {Text = "Date", Value = "Date"},
                new SelectListItem {Text = "Popularity", Value = "Popularity"}
            }, "Text", "Value", selectedSort);
        }

        public IEnumerable<GoalListViewModel> GoalList { get; set; }

        public IEnumerable<SelectListItem> FilterBy { get; set; }

        public IEnumerable<SelectListItem> SortBy { get; set; }
    }
}