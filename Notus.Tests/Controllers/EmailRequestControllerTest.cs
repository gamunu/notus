using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using Moq;
using Notus.Controllers;
using Notus.Data.Infrastructure;
using Notus.Data.Repository;
using Notus.Model.Models;
using Notus.Service;
using Notus.Tests.Helpers;
using Notus.Web.Core.Authentication;
using Notus.Web.Core.Models;
using NUnit.Framework;

namespace Notus.Tests.Controllers
{
    [TestFixture]
    public class EmailRequestControllerTest
    {
        [SetUp]
        public void SetUp()
        {
            securityTokenRepository = new Mock<ISecurityTokenRepository>();
            supportRepository = new Mock<ISupportRepository>();
            groupInvitationRepository = new Mock<IGroupInvitationRepository>();
            groupUserRepository = new Mock<IGroupUserRepository>();
            userRepository = new Mock<IUserRepository>();
            followUserRepository = new Mock<IFollowUserRepository>();
            //userProfileRepository=new Mock<UserProfileRepository>();

            unitOfWork = new Mock<IUnitOfWork>();

            tempData = new Mock<TempDataDictionary>();
            controllerContext = new Mock<ControllerContext>();
            contextBase = new Mock<HttpContextBase>();
            principal = new Mock<IPrincipal>();
            identity = new Mock<IIdentity>();
            httpRequest = new Mock<HttpRequestBase>();
            httpResponse = new Mock<HttpResponseBase>();
            genericPrincipal = new Mock<GenericPrincipal>();

            securityTokenService = new SecurityTokenService(securityTokenRepository.Object, unitOfWork.Object);
            groupInvitationService = new GroupInvitationService(groupInvitationRepository.Object, unitOfWork.Object);
            groupUserService = new GroupUserService(groupUserRepository.Object, userRepository.Object, unitOfWork.Object);
            supportService = new SupportService(supportRepository.Object, followUserRepository.Object, unitOfWork.Object);
            // userService = new UserService(userRepository.Object, unitOfWork.Object, userProfileRepository.Object);
        }

        [TearDown]
        public void TearDown()
        {
        }

        private Mock<ISecurityTokenRepository> securityTokenRepository;
        private Mock<IGroupUserRepository> groupUserRepository;
        private Mock<ISupportRepository> supportRepository;
        private Mock<IGroupInvitationRepository> groupInvitationRepository;
        private Mock<IUserRepository> userRepository;
        private Mock<IFollowUserRepository> followUserRepository;
        private Mock<UserProfileRepository> userProfileRepository;


        private Mock<IUnitOfWork> unitOfWork;

        private Mock<TempDataDictionary> tempData;
        private Mock<ControllerContext> controllerContext;
        private Mock<IIdentity> identity;
        private Mock<IPrincipal> principal;
        private Mock<HttpContextBase> contextBase;
        private Mock<HttpRequestBase> httpRequest;
        private Mock<HttpResponseBase> httpResponse;
        private Mock<GenericPrincipal> genericPrincipal;

        public ISecurityTokenService securityTokenService;
        public IGroupUserService groupUserService;
        public ISupportService supportService;
        public IGroupInvitationService groupInvitationService;
        public IUserService userService;

        public ApplicationUser getApplicationUser()
        {
            var applicationUser = new ApplicationUser
            {
                Activated = true,
                Email = "adarsh@foo.com",
                FirstName = "Adarsh",
                LastName = "Vikraman",
                UserName = "adarsh",
                RoleId = 0,
                Id = "402bd590-fdc7-49ad-9728-40efbfe512ec",
                DateCreated = DateTime.Now,
                LastLoginTime = DateTime.Now,
                ProfilePicUrl = null
            };
            return applicationUser;
        }

        [Test]
        public void Add_GroupUser()
        {
            var guidToken = Guid.NewGuid();
            var token = new SecurityToken
            {
                SecurityTokenId = 1,
                Token = guidToken,
                ActualID = 1
            };

            securityTokenRepository.Setup(x => x.Get(It.IsAny<Expression<Func<SecurityToken, bool>>>())).Returns(token);

            var user = new MemoryUser("adarsh");
            var applicationUser = getApplicationUser();
            var userContext = new UserInfo
            {
                UserId = user.Id,
                DisplayName = user.UserName,
                UserIdentifier = applicationUser.Email,
                RoleName = Enum.GetName(typeof (UserRoles), applicationUser.RoleId)
            };
            var testTicket = new FormsAuthenticationTicket(
                1,
                user.Id,
                DateTime.Now,
                DateTime.Now.Add(FormsAuthentication.Timeout),
                false,
                userContext.ToString());


            var controller = new EmailRequestController(securityTokenService, groupUserService, supportService,
                groupInvitationService);

            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            controller.ControllerContext = controllerContext.Object;

            contextBase.SetupGet(x => x.Request).Returns(httpRequest.Object);
            contextBase.SetupGet(x => x.Response).Returns(httpResponse.Object);
            genericPrincipal.Setup(x => x.Identity).Returns(identity.Object);

            contextBase.SetupGet(a => a.Response.Cookies).Returns(new HttpCookieCollection());

            var formsAuthentication = new DefaultFormsAuthentication();


            formsAuthentication.SetAuthCookie(contextBase.Object, testTicket);

            var authCookie = contextBase.Object.Response.Cookies[FormsAuthentication.FormsCookieName];

            var ticket = formsAuthentication.Decrypt(authCookie.Value);
            var goalsetterUser = new NotusUser(ticket);
            principal.Setup(x => x.Identity).Returns(goalsetterUser);

            var httprequest = new HttpRequest("", "http://yoursite/", "");
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httprequest, httpResponce);

            var sessionContainer = new HttpSessionStateContainer("id",
                new SessionStateItemCollection(),
                new HttpStaticObjectsCollection(),
                10,
                true,
                HttpCookieMode.AutoDetect,
                SessionStateMode.InProc,
                false);

            httpContext.Items["AspSession"] = typeof (HttpSessionState).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null, CallingConventions.Standard,
                new[] {typeof (HttpSessionStateContainer)},
                null)
                .Invoke(new object[] {sessionContainer});

            HttpContext.Current = httpContext;
            //HttpContext.Current.Request.Session["somevalue"];

            controller.TempData = tempData.Object;
            controller.TempData["grToken"] = guidToken;
            var result = controller.AddGroupUser() as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void Add_Support_ToGoal()
        {
            var guidToken = Guid.NewGuid();
            var token = new SecurityToken
            {
                SecurityTokenId = 1,
                Token = guidToken,
                ActualID = 1
            };

            securityTokenRepository.Setup(x => x.Get(It.IsAny<Expression<Func<SecurityToken, bool>>>())).Returns(token);

            var user = new MemoryUser("adarsh");
            var applicationUser = getApplicationUser();
            var userContext = new UserInfo
            {
                UserId = user.Id,
                DisplayName = user.UserName,
                UserIdentifier = applicationUser.Email,
                RoleName = Enum.GetName(typeof (UserRoles), applicationUser.RoleId)
            };
            var testTicket = new FormsAuthenticationTicket(
                1,
                user.Id,
                DateTime.Now,
                DateTime.Now.Add(FormsAuthentication.Timeout),
                false,
                userContext.ToString());


            var controller = new EmailRequestController(securityTokenService, groupUserService, supportService,
                groupInvitationService);

            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controllerContext.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            controller.ControllerContext = controllerContext.Object;

            contextBase.SetupGet(x => x.Request).Returns(httpRequest.Object);
            contextBase.SetupGet(x => x.Response).Returns(httpResponse.Object);
            genericPrincipal.Setup(x => x.Identity).Returns(identity.Object);

            contextBase.SetupGet(a => a.Response.Cookies).Returns(new HttpCookieCollection());

            var formsAuthentication = new DefaultFormsAuthentication();


            formsAuthentication.SetAuthCookie(contextBase.Object, testTicket);

            var authCookie = contextBase.Object.Response.Cookies[FormsAuthentication.FormsCookieName];

            var ticket = formsAuthentication.Decrypt(authCookie.Value);
            var goalsetterUser = new NotusUser(ticket);
            principal.Setup(x => x.Identity).Returns(goalsetterUser);

            var httprequest = new HttpRequest("", "http://yoursite/", "");
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httprequest, httpResponce);

            var sessionContainer = new HttpSessionStateContainer("id",
                new SessionStateItemCollection(),
                new HttpStaticObjectsCollection(),
                10,
                true,
                HttpCookieMode.AutoDetect,
                SessionStateMode.InProc,
                false);

            httpContext.Items["AspSession"] = typeof (HttpSessionState).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null, CallingConventions.Standard,
                new[] {typeof (HttpSessionStateContainer)},
                null)
                .Invoke(new object[] {sessionContainer});

            HttpContext.Current = httpContext;

            controller.TempData = tempData.Object;
            controller.TempData["goToken"] = guidToken;
            var result = controller.AddSupportToGoal() as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}