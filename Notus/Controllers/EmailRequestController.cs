using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Notus.Helpers;
using Notus.Model.Models;
using Notus.Service;

namespace Notus.Controllers
{
    [Authorize]
    public class EmailRequestController : Controller
    {
        public readonly IGroupInvitationService groupInvitationService;
        public readonly IGroupUserService groupUserService;
        //
        // GET: /EmailRequest/
        private readonly ISecurityTokenService securityTokenService;
        public readonly ISupportService supportService;

        public EmailRequestController(ISecurityTokenService securityTokenService, IGroupUserService groupUserService,
            ISupportService supportService, IGroupInvitationService groupInvitationService)
        {
            this.securityTokenService = securityTokenService;
            this.groupUserService = groupUserService;
            this.supportService = supportService;
            this.groupInvitationService = groupInvitationService;
        }

        public ActionResult AddGroupUser()
        {
            var groupIdToken = (Guid) TempData["grToken"];
            var groupId = securityTokenService.GetActualId(groupIdToken);
            var newGroupUser = new GroupUser
            {
                UserId = User.Identity.GetUserId(),
                GroupId = groupId,
                Admin = false
            };
            groupUserService.CreateGroupUser(newGroupUser, groupInvitationService);
            securityTokenService.DeleteSecurityToken(groupIdToken);
            NotusSessionFacade.Remove(NotusSessionFacade.JoinGroupOrGoal);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AddSupportToGoal()
        {
            var goalIdToken = (Guid) TempData["goToken"];
            var goalId = securityTokenService.GetActualId(goalIdToken);
            supportService.CreateSupport(new Support
            {
                UserId = User.Identity.GetUserId(),
                GoalId = goalId,
                SupportedDate = DateTime.Now
            });
            securityTokenService.DeleteSecurityToken(goalIdToken);
            NotusSessionFacade.Remove(NotusSessionFacade.JoinGroupOrGoal);
            return RedirectToAction("Index", "Home");
        }
    }
}