using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Moq;
using Notus.Controllers;
using Notus.Data.Infrastructure;
using Notus.Data.Repository;
using Notus.Model.Models;
using Notus.Service;
using Notus.ViewModels;
using NUnit.Framework;

namespace Notus.Tests.Controllers
{
    [TestFixture]
    public class SearchControllerTest
    {
        [SetUp]
        public void SetUp()
        {
            goalRepository = new Mock<IGoalRepository>();
            userRepository = new Mock<IUserRepository>();
            groupRepository = new Mock<IGroupRepository>();
            followUserRepository = new Mock<IFollowUserRepository>();
            groupUserRepository = new Mock<IGroupUserRepository>();
            userProfileRepository = new Mock<IUserProfileRepository>();
            unitOfWork = new Mock<IUnitOfWork>();

            goalService = new GoalService(goalRepository.Object, followUserRepository.Object, unitOfWork.Object);
            groupService = new GroupService(groupRepository.Object, followUserRepository.Object,
                groupUserRepository.Object, unitOfWork.Object);
            userService = new UserService(userRepository.Object, unitOfWork.Object, userProfileRepository.Object);
        }

        [TearDown]
        public void TearDown()
        {
        }

        private Mock<IGoalRepository> goalRepository;
        private Mock<IUserRepository> userRepository;
        private Mock<IGroupRepository> groupRepository;
        private Mock<IFollowUserRepository> followUserRepository;
        private Mock<IGroupUserRepository> groupUserRepository;
        private Mock<IUserProfileRepository> userProfileRepository;

        private IGoalService goalService;
        private IGroupService groupService;
        private IUserService userService;

        private Mock<IUnitOfWork> unitOfWork;

        [Test]
        public void Search_All()
        {
            var fakegoal = new List<Goal>
            {
                new Goal {GoalStatusId = 1, GoalName = "a", GoalType = false},
                new Goal {GoalStatusId = 1, GoalName = "abc", GoalType = false},
                new Goal {GoalStatusId = 1, GoalName = "aedg", GoalType = false}
            }.AsEnumerable();
            goalRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<Goal, bool>>>())).Returns(fakegoal);

            var fakeUser = new List<ApplicationUser>
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
            userRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<ApplicationUser, bool>>>())).Returns(fakeUser);

            var fakeGroups = new List<Group>
            {
                new Group {GroupName = "Test1", Description = "Test1Desc"},
                new Group {GroupName = "Test2", Description = "Test2Desc"},
                new Group {GroupName = "Test3", Description = "Test3Desc"}
            }.AsEnumerable();
            groupRepository.Setup(x => x.GetMany(It.IsAny<Expression<Func<Group, bool>>>())).Returns(fakeGroups);

            Mapper.CreateMap<Goal, GoalViewModel>();
            Mapper.CreateMap<Group, GroupViewModel>();
            var controller = new SearchController(goalService, userService, groupService);
            var result = controller.SearchAll("a");
            Assert.IsNotNull(result, "View Result is null");
            Assert.IsInstanceOf(typeof (SearchViewModel),
                result.ViewData.Model, "Wrong View Model");
        }
    }
}