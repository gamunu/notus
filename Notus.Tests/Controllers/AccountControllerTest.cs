using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Moq;
using Mvc.Mailer;
using Notus.Controllers;
using Notus.Data.Infrastructure;
using Notus.Data.Repository;
using Notus.Mailers;
using Notus.Model.Models;
using Notus.Models;
using Notus.Service;
using Notus.Tests.Helpers;
using Notus.ViewModels;
using Notus.Web.Core.Authentication;
using Notus.Web.Core.Models;
using NUnit.Framework;

namespace Notus.Tests.Controllers
{
    [TestFixture]
    public class AccountControllerTest
    {
        [SetUp]
        public void SetUp()
        {
            userRepository = new Mock<IUserRepository>();
            userProfileRepository = new Mock<IUserProfileRepository>();
            followRequestRepository = new Mock<IFollowRequestRepository>();
            followUserRepository = new Mock<IFollowUserRepository>();
            securityTokenRepository = new Mock<ISecurityTokenRepository>();


            unitOfWork = new Mock<IUnitOfWork>();

            userService = new UserService(userRepository.Object, unitOfWork.Object, userProfileRepository.Object);
            userProfileService = new UserProfileService(userProfileRepository.Object, unitOfWork.Object);
            followRequestService = new FollowRequestService(followRequestRepository.Object, unitOfWork.Object);
            followUserService = new FollowUserService(followUserRepository.Object, unitOfWork.Object);
            securityTokenService = new SecurityTokenService(securityTokenRepository.Object, unitOfWork.Object);

            controllerContext = new Mock<ControllerContext>();
            contextBase = new Mock<HttpContextBase>();
            httpRequest = new Mock<HttpRequestBase>();
            httpResponse = new Mock<HttpResponseBase>();
            genericPrincipal = new Mock<GenericPrincipal>();
            httpSession = new Mock<HttpSessionStateBase>();
            authentication = new Mock<IFormsAuthentication>();


            identity = new Mock<IIdentity>();
            principal = new Mock<IPrincipal>();
            tempData = new Mock<TempDataDictionary>();
            file = new Mock<HttpPostedFileBase>();
            stream = new Mock<Stream>();
            accountController = new Mock<AccountController>();
        }

        [TearDown]
        public void TearDown()
        {
            TestSmtpClient.SentMails.Clear();
        }

        private Mock<IUserRepository> userRepository;
        private Mock<IUserProfileRepository> userProfileRepository;
        private Mock<IFollowRequestRepository> followRequestRepository;
        private Mock<IFollowUserRepository> followUserRepository;
        private Mock<ISecurityTokenRepository> securityTokenRepository;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<ControllerContext> controllerContext;
        private Mock<IIdentity> identity;
        private Mock<IPrincipal> principal;
        private Mock<HttpContext> httpContext;
        private Mock<HttpContextBase> contextBase;
        private Mock<HttpRequestBase> httpRequest;
        private Mock<HttpResponseBase> httpResponse;
        private Mock<HttpSessionStateBase> httpSession;
        private Mock<GenericPrincipal> genericPrincipal;

        private Mock<TempDataDictionary> tempData;
        private Mock<HttpPostedFileBase> file;
        private Mock<Stream> stream;
        private Mock<IFormsAuthentication> authentication;

        private IUserService userService;
        private IUserProfileService userProfileService;
        private IGoalService goalService;
        private IUpdateService updateService;
        private ICommentService commentService;
        private IFollowRequestService followRequestService;
        private IFollowUserService followUserService;
        private ISecurityTokenService securityTokenService;
        private IUserMailer userMailer = new UserMailer();
        private Mock<AccountController> accountController;

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
        public void Accept_Request()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());
            var applicationUser = getApplicationUser();
            userRepository.Setup(x => x.Get(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                .Returns(applicationUser);
            var contr = new AccountController(userService, userProfileService, goalService, updateService,
                commentService, followRequestService, followUserService, securityTokenService, userManager);
            var result =
                contr.AcceptRequest("402bd590-fdc7-49ad-9728-40efbfe512ed", "402bd590-fdc7-49ad-9728-40efbfe512ec") as
                    RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void Delete_Follow_Request()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());
            var request = new FollowRequest
            {
                FromUserId = "402bd590-fdc7-49ad-9728-40efbfe512ec",
                ToUserId = "402bd590-fdc7-49ad-9728-40efbfe512ed"
            };
            followRequestRepository.Setup(x => x.Get(It.IsAny<Expression<Func<FollowRequest, bool>>>()))
                .Returns(request);
            var contr = new AccountController(userService, userProfileService, goalService, updateService,
                commentService, followRequestService, followUserService, securityTokenService, userManager);
            var result =
                contr.RejectRequest("402bd590-fdc7-49ad-9728-40efbfe512ed", "402bd590-fdc7-49ad-9728-40efbfe512ec") as
                    RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void Edit_Basic_Info()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());
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
            var controller = new AccountController(userService, userProfileService, goalService, updateService,
                commentService, followRequestService, followUserService, securityTokenService, userManager);
            principal.SetupGet(x => x.Identity.Name).Returns("adarsh");
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
            string[] userRoles = {goalsetterUser.RoleName};
            principal.Setup(x => x.Identity).Returns(goalsetterUser);
            var userProfile = new UserProfile
            {
                UserId = "402bd590-fdc7-49ad-9728-40efbfe512ec",
                FirstName = "Adarsh",
                LastName = "Vikraman",
                Email = "adarsh@foo.com"
            };
            userProfileRepository.Setup(x => x.Get(It.IsAny<Expression<Func<UserProfile, bool>>>()))
                .Returns(userProfile);
            Mapper.CreateMap<UserProfile, UserProfileFormModel>();
            var result = controller.EditBasicInfo() as PartialViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<UserProfileFormModel>(result.ViewData.Model, "WrongType");
            var data = result.ViewData.Model as UserProfileFormModel;
            Assert.AreEqual("adarsh@foo.com", data.Email);
        }

        [Test]
        public void Edit_Personal_Info()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());
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
            var controller = new AccountController(userService, userProfileService, goalService, updateService,
                commentService, followRequestService, followUserService, securityTokenService, userManager);
            principal.SetupGet(x => x.Identity.Name).Returns("adarsh");
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
            string[] userRoles = {goalsetterUser.RoleName};
            principal.Setup(x => x.Identity).Returns(goalsetterUser);
            var grpuser = new UserProfile
            {
                UserId = "402bd590-fdc7-49ad-9728-40efbfe512ec",
                Address = "t",
                City = "t",
                State = "adarsh@foo.com"
            };
            userProfileRepository.Setup(x => x.Get(It.IsAny<Expression<Func<UserProfile, bool>>>())).Returns(grpuser);
            Mapper.CreateMap<UserProfile, UserProfileFormModel>();

            var result = controller.EditPersonalInfo() as PartialViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<UserProfileFormModel>(result.ViewData.Model, "WrongType");
            var data = result.ViewData.Model as UserProfileFormModel;
            Assert.AreEqual("t", data.Address);
        }

        [Test]
        public void Editprofile_Post()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());
            var applicationUser = getApplicationUser();
            userRepository.Setup(x => x.Get(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                .Returns(applicationUser);
            var profile = new UserProfileFormModel();

            Mapper.CreateMap<UserProfileFormModel, UserProfile>();
            profile.FirstName = "adarsh";

            var contr = new AccountController(userService, userProfileService, goalService, updateService,
                commentService, followRequestService, followUserService, securityTokenService, userManager);
            var result = contr.EditProfile(profile) as RedirectToRouteResult;

            Assert.AreEqual("UserProfile", result.RouteValues["action"]);
        }

        [Test]
        public void Follow_Request()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());
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


            var controller = new AccountController(userService, userProfileService, goalService, updateService,
                commentService, followRequestService, followUserService, securityTokenService, userManager);
            principal.SetupGet(x => x.Identity.Name).Returns("adarsh");
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
            string[] userRoles = {goalsetterUser.RoleName};

            principal.Setup(x => x.Identity).Returns(goalsetterUser);

            userRepository.Setup(x => x.Get(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                .Returns(applicationUser);
            Mapper.CreateMap<FollowRequestFormModel, FollowRequest>();


            var result = controller.FollowRequest("402bd590-fdc7-49ad-9728-40efbfe512ec") as RedirectToRouteResult;
            Assert.AreEqual("UserProfile", result.RouteValues["action"]);
        }

        [Test]
        public void Followers_List()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());
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


            var controller = new AccountController(userService, userProfileService, goalService, updateService,
                commentService, followRequestService, followUserService, securityTokenService, userManager);

            principal.SetupGet(x => x.Identity.Name).Returns("adarsh");
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
            string[] userRoles = {goalsetterUser.RoleName};

            principal.Setup(x => x.Identity).Returns(goalsetterUser);
            IEnumerable<FollowUser> fakeuser = new List<FollowUser>
            {
                new FollowUser
                {
                    FollowUserId = 1,
                    Accepted = false,
                    FromUserId = "402bd590-fdc7-49ad-9728-40efbfe512ed",
                    ToUserId = "402bd590-fdc7-49ad-9728-40efbfe512ec"
                },
                new FollowUser
                {
                    FollowUserId = 2,
                    Accepted = false,
                    FromUserId = "402bd590-fdc7-49ad-9728-40efbfe512ee",
                    ToUserId = "402bd590-fdc7-49ad-9728-40efbfe512ec"
                }
            };
            followUserRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<FollowUser, bool>>>())).Returns(fakeuser);

            Mapper.CreateMap<ApplicationUser, FollowersViewModel>();


            var result = controller.Followers() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<FollowersViewModel>>(result.ViewData.Model, "WrongType");
            var data = result.ViewData.Model as IEnumerable<FollowersViewModel>;
            Assert.AreEqual(2, data.Count());
        }


        [Test]
        public void Followings_list()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());
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


            var controller = new AccountController(userService, userProfileService, goalService, updateService,
                commentService, followRequestService, followUserService, securityTokenService, userManager);

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

            IEnumerable<FollowUser> fakeuser = new List<FollowUser>
            {
                new FollowUser
                {
                    FollowUserId = 1,
                    Accepted = false,
                    FromUserId = "402bd590-fdc7-49ad-9728-40efbfe512ec",
                    ToUserId = "402bd590-fdc7-49ad-9728-40efbfe512ed"
                },
                new FollowUser
                {
                    FollowUserId = 2,
                    Accepted = false,
                    FromUserId = "402bd590-fdc7-49ad-9728-40efbfe512ec",
                    ToUserId = "402bd590-fdc7-49ad-9728-40efbfe512ee"
                }
            };
            followUserRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<FollowUser, bool>>>())).Returns(fakeuser);

            Mapper.CreateMap<ApplicationUser, FollowersViewModel>();


            var result = controller.Followings() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<FollowingViewModel>>(result.ViewData.Model, "WrongType");
            var data = result.ViewData.Model as IEnumerable<FollowingViewModel>;
            Assert.AreEqual(2, data.Count());
        }

        [Test]
        public void Image_Upload_GetView()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());
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
            var controller = new AccountController(userService, userProfileService, goalService, updateService,
                commentService, followRequestService, followUserService, securityTokenService, userManager);
            principal.SetupGet(x => x.Identity.Name).Returns("adarsh");
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
            string[] userRoles = {goalsetterUser.RoleName};
            principal.Setup(x => x.Identity).Returns(goalsetterUser);
            userRepository.Setup(x => x.Get(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                .Returns(applicationUser);
            var result = controller.ImageUpload() as PartialViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<UploadImageViewModel>(result.ViewData.Model, "Wrong model");
            var data = result.ViewData.Model as UploadImageViewModel;
            Assert.AreEqual(null, data.LocalPath, "not matching");
        }

        [Test]
        public void Login_Get_View_If_Guid_Is_NotNull()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());
            //mocking QueryString
            var querystring = new NameValueCollection {{"guid", "got_value"}};
            var querystring1 = new NameValueCollection {{"reg", "value"}};
            var goalIdToken = Guid.NewGuid();
            // Guid 
            controllerContext.SetupGet(p => p.HttpContext.Request.QueryString).Returns(querystring);
            //controllerContext.SetupGet(p => p.HttpContext.Request.QueryString).Returns(querystring1);
            controllerContext.SetupGet(p => p.HttpContext.Session).Returns(httpSession.Object);
            var controller = new AccountController(userService, userProfileService, goalService, updateService,
                commentService, followRequestService, followUserService, securityTokenService, userManager);
            controller.ControllerContext = controllerContext.Object;

            var httprequest = new HttpRequest("", "http://localhost/", "");
            var stringWriter = new StringWriter();
            var httpResponce = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httprequest, httpResponce);
            // Mocking HttpContext.Current
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

            var rslt = controller.Login("abcd") as ViewResult;
            Assert.IsNotNull(rslt);
        }

        [Test]
        public void Login_Get_View_If_Guid_Is_Null()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());
            var goalIdToken = Guid.NewGuid();
            controllerContext.SetupGet(p => p.HttpContext.Request.QueryString).Returns(new NameValueCollection());
            var controller = new AccountController(userService, userProfileService, goalService, updateService,
                commentService, followRequestService, followUserService, securityTokenService, userManager);
            controller.ControllerContext = controllerContext.Object;
            var rslt = controller.Login("abcd") as ViewResult;
            Assert.IsNotNull(rslt);
        }

        [Test]
        public void LoginTest()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());

            var controller = new AccountController(userService, userProfileService, goalService, updateService,
                commentService, followRequestService, followUserService, securityTokenService, userManager);
            var mockAuthenticationManager = new Mock<IAuthenticationManager>();
            mockAuthenticationManager.Setup(am => am.SignOut());
            mockAuthenticationManager.Setup(am => am.SignIn());
            controller.AuthenticationManager = mockAuthenticationManager.Object;
            var applicationUser = getApplicationUser();
            userManager.CreateAsync(applicationUser, "123456");
            var result =
                controller.Login(new LoginViewModel {Email = "adarsh", Password = "123456", RememberMe = false}, "abcd")
                    .Result;
            Assert.IsNotNull(result);
            var addedUser = userManager.FindByName("adarsh");
            Assert.IsNotNull(addedUser);
            Assert.AreEqual("adarsh", addedUser.UserName);
        }

        [Test]
        public void LogOff()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());
            var mockAuthenticationManager = new Mock<IAuthenticationManager>();
            mockAuthenticationManager.Setup(am => am.SignOut());
            mockAuthenticationManager.Setup(am => am.SignIn());
            var controller = new AccountController(userService, userProfileService, goalService, updateService,
                commentService, followRequestService, followUserService, securityTokenService, userManager);
            controller.AuthenticationManager = mockAuthenticationManager.Object;
            var httprequest = new HttpRequest("", "http://localhost/", "");
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

            var result = controller.LogOff() as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void Register_Get_Returns_View()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());
            var controller = new AccountController(userService, userProfileService, goalService, updateService,
                commentService, followRequestService, followUserService, securityTokenService, userManager);
            var rslt = controller.Register() as ViewResult;
            Assert.IsNotNull(rslt);
        }

        [Test]
        public void RegisterTest()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());
            var controller = new AccountController(userService, userProfileService, goalService, updateService,
                commentService, followRequestService, followUserService, securityTokenService, userManager);
            var mockAuthenticationManager = new Mock<IAuthenticationManager>();
            mockAuthenticationManager.Setup(am => am.SignOut());
            mockAuthenticationManager.Setup(am => am.SignIn());
            controller.AuthenticationManager = mockAuthenticationManager.Object;
            var httprequest = new HttpRequest("", "http://localhost/", "");
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
            var result =
                controller.Register(new RegisterViewModel
                {
                    UserName = "adarsh",
                    Password = "123456",
                    ConfirmPassword = "123456"
                }).Result;
            Assert.IsNotNull(result);
            var addedUser = userManager.FindByName("adarsh");
            Assert.IsNotNull(addedUser);
            Assert.AreEqual("adarsh", addedUser.UserName);
        }

        [Test]
        public void SearchUser()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());
            var fake = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Activated = true,
                    Email = "user1@foo.com",
                    FirstName = "user1",
                    LastName = "user1",
                    RoleId = 0
                },
                new ApplicationUser
                {
                    Activated = true,
                    Email = "user2@foo.com",
                    FirstName = "user2",
                    LastName = "user2",
                    RoleId = 0
                },
                new ApplicationUser
                {
                    Activated = true,
                    Email = "user3@foo.com",
                    FirstName = "user3",
                    LastName = "user3",
                    RoleId = 0
                },
                new ApplicationUser
                {
                    Activated = true,
                    Email = "user4@foo.com",
                    FirstName = "user4",
                    LastName = "user4",
                    RoleId = 0
                }
            }.AsEnumerable();
            userRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<ApplicationUser, bool>>>())).Returns(fake);
            var contr = new AccountController(userService, userProfileService, goalService, updateService,
                commentService, followRequestService, followUserService, securityTokenService, userManager);
            var result = contr.SearchUser("u");
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count(), "not matching");
        }

        [Test]
        public void UnFollow()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());
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


            var controller = new AccountController(userService, userProfileService, goalService, updateService,
                commentService, followRequestService, followUserService, securityTokenService, userManager);

            principal.SetupGet(x => x.Identity.Name).Returns("adarsh");
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
            string[] userRoles = {goalsetterUser.RoleName};

            principal.Setup(x => x.Identity).Returns(goalsetterUser);
            var flwuser = new FollowUser
            {
                FromUserId = "402bd590-fdc7-49ad-9728-40efbfe512ec",
                ToUserId = "402bd590-fdc7-49ad-9728-40efbfe512ed"
            };
            followUserRepository.Setup(x => x.Get(It.IsAny<Expression<Func<FollowUser, bool>>>())).Returns(flwuser);

            var result = controller.Unfollow("402bd590-fdc7-49ad-9728-40efbfe512ed") as RedirectToRouteResult;
            Assert.AreEqual("UserProfile", result.RouteValues["action"]);
        }

        [Test]
        public void Upload_Image_Post()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());
            var image = new UploadImageViewModel
            {
                IsFile = true,
                UserId = "402bd590-fdc7-49ad-9728-40efbfe512ec",
                LocalPath = "dddd"
            };
            var controller = new AccountController(userService, userProfileService, goalService, updateService,
                commentService, followRequestService, followUserService, securityTokenService, userManager);
            var result = controller.UploadImage(image) as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<UploadImageViewModel>(result.ViewData.Model, "WrongType");
            Assert.AreEqual("ImageUpload", result.ViewName);
        }

        [Test]
        public void UserProfile()
        {
            var userManager = new UserManager<ApplicationUser>(new TestUserStore());
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
            userRepository.Setup(x => x.Get(It.IsAny<Expression<Func<ApplicationUser, bool>>>()))
                .Returns(applicationUser);
            var controller = new AccountController(userService, userProfileService, goalService, updateService,
                commentService, followRequestService, followUserService, securityTokenService, userManager);
            principal.SetupGet(x => x.Identity.Name).Returns("adarsh");
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
            string[] userRoles = {goalsetterUser.RoleName};
            principal.Setup(x => x.Identity).Returns(goalsetterUser);
            var prfil = new UserProfile
            {
                FirstName = "Adarsh",
                LastName = "Vikraman",
                DateOfBirth = DateTime.Now,
                Gender = true,
                Address = "a",
                City = "a",
                State = "a",
                Country = "a",
                ZipCode = 2344545,
                ContactNo = 1223344556,
                UserId = "402bd590-fdc7-49ad-9728-40efbfe512ec"
            };
            userProfileRepository.Setup(x => x.Get(It.IsAny<Expression<Func<UserProfile, bool>>>())).Returns(prfil);
            IEnumerable<FollowRequest> fake = new List<FollowRequest>
            {
                new FollowRequest
                {
                    FollowRequestId = 1,
                    FromUserId = "402bd590-fdc7-49ad-9728-40efbfe512ec",
                    ToUserId = "402bd590-fdc7-49ad-9728-40efbfe512ed"
                },
                new FollowRequest
                {
                    FollowRequestId = 2,
                    FromUserId = "402bd590-fdc7-49ad-9728-40efbfe512ec",
                    ToUserId = "402bd590-fdc7-49ad-9728-40efbfe512ee"
                }
            };
            followRequestRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<FollowRequest, bool>>>()))
                .Returns(fake);
            IEnumerable<FollowUser> fakeuser = new List<FollowUser>
            {
                new FollowUser
                {
                    FollowUserId = 1,
                    Accepted = false,
                    FromUserId = "402bd590-fdc7-49ad-9728-40efbfe512ec",
                    ToUserId = "402bd590-fdc7-49ad-9728-40efbfe512ed"
                },
                new FollowUser
                {
                    FollowUserId = 2,
                    Accepted = false,
                    FromUserId = "402bd590-fdc7-49ad-9728-40efbfe512ec",
                    ToUserId = "402bd590-fdc7-49ad-9728-40efbfe512ee"
                }
            };
            followUserRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<FollowUser, bool>>>())).Returns(fakeuser);
            var result = controller.UserProfile("402bd590-fdc7-49ad-9728-40efbfe512ec");
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<UserProfileViewModel>(result.ViewData.Model, "WrongType");
            var data = result.ViewData.Model as UserProfileViewModel;
            Assert.AreEqual("adarsh", data.UserName);
        }
    }
}