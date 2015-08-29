using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Notus.Service;
using Notus.Services;
using Notus.ViewModels;

namespace Notus.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly CreateNotificationList notificationListCreation = new CreateNotificationList();
        private readonly ICommentService commentService;
        private readonly ICommentUserService commentUserService;
        private IFocusService focusService;
        private readonly IFollowUserService followUserService;
        private readonly IGoalService goalService;
        private readonly IGroupCommentService groupcommentService;
        private readonly IGroupCommentUserService groupCommentUserService;
        private readonly IGroupGoalService groupGoalService;
        private readonly IGroupService groupService;
        private readonly IGroupUpdateService groupupdateService;
        private readonly IGroupUpdateUserService groupUpdateUserService;
        private readonly IGroupUserService groupUserService;
        private IMetricService metricService;
        private readonly ISupportService supportService;
        private readonly IUpdateService updateService;
        private readonly IUserService userService;

        public HomeController(IMetricService metricService, IFocusService focusService, IGoalService goalService,
            ICommentService commentService, IUpdateService updateService, ISupportService supportService,
            IUserService userService, IGroupUserService groupUserService, IGroupService groupService,
            IGroupGoalService groupGoalService, IGroupUpdateService groupupdateService,
            IGroupCommentService groupcommentService, IFollowUserService followUserService,
            IGroupUpdateUserService groupUpdateUserService, IGroupCommentUserService groupCommentUserService,
            ICommentUserService commentUserService)
        {
            this.metricService = metricService;
            this.focusService = focusService;
            this.goalService = goalService;
            this.commentService = commentService;
            this.updateService = updateService;
            this.supportService = supportService;
            this.userService = userService;
            this.groupService = groupService;
            this.groupUserService = groupUserService;
            this.groupGoalService = groupGoalService;
            this.groupupdateService = groupupdateService;
            this.groupcommentService = groupcommentService;
            this.followUserService = followUserService;
            this.groupCommentUserService = groupCommentUserService;
            this.groupUpdateUserService = groupUpdateUserService;
            this.commentUserService = commentUserService;
        }

        /// <summary>
        ///     returns all notifications on first load
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(int page = 0)
        {
            var noOfRecords = 10;
            var dashboard = new HomeViewModel
            {
                Notification = GetNotifications(page, noOfRecords),
                Count = GetNotifications(page, noOfRecords).Count()
            };

            if (Request.IsAjaxRequest())
            {
                if (dashboard.Count != 0)
                    return PartialView("Notification", dashboard);
                return null;
            }
            return View(dashboard);
        }


        public ViewResult About()
        {
            //ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ViewResult Contact()
        {
            //ViewBag.Message = "Your quintessential contact page.";

            return View();
        }


        public PartialViewResult UserNotification(string id)
        {
            var dashboard = new HomeViewModel
            {
                Notification = GetNotifications(id)
            };
            return PartialView("Notification", dashboard);
        }


        /// <summary>
        ///     get notifications paged
        /// </summary>
        /// <param name="?"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public IEnumerable<NotificationsViewModel> GetNotifications(int page, int noOfRecords)
        {
            var notifications = notificationListCreation.GetNotifications(User.Identity.GetUserId(), goalService,
                commentService, updateService, supportService, userService, groupService, groupUserService,
                groupGoalService, groupcommentService, groupupdateService, followUserService, groupCommentUserService,
                commentUserService, groupUpdateUserService);

            var skipNotifications = noOfRecords*page;
            notifications = notifications.Skip(skipNotifications).Take(noOfRecords);

            return notifications;
        }

        public IEnumerable<NotificationsViewModel> GetNotifications(string userid)
        {
            var notifications = notificationListCreation.GetProfileNotifications(userid, goalService, commentService,
                updateService, supportService, userService, groupService, groupUserService, groupGoalService,
                groupcommentService, groupupdateService, commentUserService);
            return notifications;
        }
    }
}