﻿using Notus.Data.Infrastructure;
using Notus.Data.Repository;
using Notus.Model.Models;

namespace Notus.Service
{
    public interface IGroupCommentUserService
    {
        void CreateGroupCommentUser(string userId, int groupCommentId);
        void DeleteGroupCommentUser(string userId, int groupCommentId);
        GroupCommentUser GetCommentUser(int commentId);

        ApplicationUser GetGroupCommentUser(int groupCommentId);
        void DeleteGroupCommentUser(int id);
        void SaveGroupCommentUser();
    }

    public class GroupCommentUserService : IGroupCommentUserService
    {
        private readonly IGroupCommentUserRepository groupCommentUserRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;

        public GroupCommentUserService(IGroupCommentUserRepository groupCommentUserRepository, IUnitOfWork unitOfWork,
            IUserRepository userRepository)
        {
            this.groupCommentUserRepository = groupCommentUserRepository;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        #region IGroupCommentUserService Members

        public void DeleteGroupCommentUser(string userId, int groupCommentId)
        {
            var groupCommentUser =
                groupCommentUserRepository.Get(cu => cu.UserId == userId && cu.GroupCommentId == groupCommentId);
            groupCommentUserRepository.Delete(groupCommentUser);
            SaveGroupCommentUser();
        }

        public GroupCommentUser GetCommentUser(int commentId)
        {
            return groupCommentUserRepository.Get(gcu => gcu.GroupCommentId == commentId);
        }

        public void DeleteGroupCommentUser(int id)
        {
            var groupCommentUser = groupCommentUserRepository.GetById(id);
            groupCommentUserRepository.Delete(groupCommentUser);
            SaveGroupCommentUser();
        }


        public void SaveGroupCommentUser()
        {
            unitOfWork.Commit();
        }


        public void CreateGroupCommentUser(string userId, int groupCommentId)
        {
            var groupCommentUser = new GroupCommentUser {UserId = userId, GroupCommentId = groupCommentId};
            groupCommentUserRepository.Add(groupCommentUser);
            SaveGroupCommentUser();
        }


        public ApplicationUser GetGroupCommentUser(int groupCommentId)
        {
            var groupCommentUserId = groupCommentUserRepository.Get(g => g.GroupCommentId == groupCommentId).UserId;
            return userRepository.GetById(groupCommentUserId);
        }

        #endregion
    }
}