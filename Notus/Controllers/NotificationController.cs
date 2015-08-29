using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Notus.Model.Models;
using Notus.Service;
using Notus.ViewModels;

namespace Notus.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        private ICommentService _commentService;
        private readonly IFollowRequestService _followRequestService;
        private IGoalService _goalService;
        private readonly IGroupInvitationService _groupInvitationService;
        private readonly ISupportInvitationService _supportInvitationService;
        private IUpdateService _updateService;
        private readonly IUserService _userService;

        public NotificationController(IGoalService goalService, IUpdateService updateService,
            ICommentService commentService, IGroupInvitationService groupInvitationService,
            ISupportInvitationService supportInvitationService, IFollowRequestService followRequestService,
            IUserService userService)
        {
            this._goalService = goalService;
            this._supportInvitationService = supportInvitationService;
            this._updateService = updateService;
            this._groupInvitationService = groupInvitationService;
            this._commentService = commentService;
            this._followRequestService = followRequestService;
            this._userService = userService;
        }


        /// <summary>
        ///     Action that returns invitations
        /// </summary>
        /// <param name="page">current page no will be bere</param>
        /// <returns></returns>
        public ActionResult Index(int page = 0)
        {
            var noOfrecords = 10;
            var notifications = GetNotifications(page, noOfrecords);
            if (Request.IsAjaxRequest())
            {
                return PartialView("_NotificationList", notifications);
            }
            return View("Index", notifications);
        }


        public int GetNumberOfInvitations()
        {
            return _groupInvitationService.GetGroupInvitationsForUser(User.Identity.GetUserId()).Count() +
                   _supportInvitationService.GetSupportInvitationsForUser(User.Identity.GetUserId()).Count() +
                   _followRequestService.GetFollowRequestsForUser(User.Identity.GetUserId()).Count();
        }

        /// <summary>
        ///     Method returns paged notifications
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public IEnumerable<NotificationViewModel> GetNotifications(int page, int noOfRecords)
        {
            var groupInv = _groupInvitationService.GetGroupInvitationsForUser(User.Identity.GetUserId());
            var supportInv = _supportInvitationService.GetSupportInvitationsForUser(User.Identity.GetUserId());
            var followRequests = _followRequestService.GetFollowRequests(User.Identity.GetUserId());
            var invitations = Mapper.Map<IEnumerable<GroupInvitation>, IEnumerable<NotificationViewModel>>(groupInv);
            invitations =
                invitations.Concat(
                    Mapper.Map<IEnumerable<SupportInvitation>, IEnumerable<NotificationViewModel>>(supportInv));
            invitations =
                invitations.Concat(
                    Mapper.Map<IEnumerable<FollowRequest>, IEnumerable<NotificationViewModel>>(followRequests));
            foreach (var item in invitations)
            {
                var fromUser = _userService.GetUser(item.FromUserId);
                var toUser = _userService.GetUser(item.ToUserId);
                item.FromUser = fromUser;
                item.ToUser = toUser;
            }

            //for paging

            var skipNotifications = noOfRecords*page;
            invitations = invitations.Skip(skipNotifications).Take(noOfRecords);
            return invitations;
        }
    }
}