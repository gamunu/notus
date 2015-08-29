using System.Collections.Generic;
using Notus.Data.Infrastructure;
using Notus.Data.Repository;
using Notus.Model.Models;

namespace Notus.Service
{
    public interface ICommentUserService
    {
        IEnumerable<int> GetCommentIdsByUser(string userId);
        ApplicationUser GetUser(int commentId);
        void CreateCommentUser(string userId, int commentId);
        void DeleteCommentUser(string userId, int commentId);

        void DeleteCommentUser(int id);
        void SaveCommentUser();
    }

    public class CommentUserService : ICommentUserService
    {
        private readonly ICommentUserRepository commentUserRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;

        public CommentUserService(ICommentUserRepository commentUserRepository, IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            this.commentUserRepository = commentUserRepository;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }


        public void CreateCommentUser(string userId, int commentId)
        {
            var commentUser = new CommentUser {UserId = userId, CommentId = commentId};
            commentUserRepository.Add(commentUser);
            SaveCommentUser();
        }

        #region ICommentUserService Members

        public ApplicationUser GetUser(int commentId)
        {
            var userId = commentUserRepository.Get(cu => cu.CommentId == commentId).UserId;
            return userRepository.GetById(userId);
        }

        public IEnumerable<int> GetCommentIdsByUser(string userId)
        {
            var commentIds = new List<int>();
            var commentUsers = commentUserRepository.GetMany(c => c.UserId == userId);
            foreach (var item in commentUsers)
            {
                commentIds.Add(item.CommentId);
            }
            return commentIds;
        }

        public void DeleteCommentUser(string userId, int commentId)
        {
            var commentUser = commentUserRepository.Get(cu => cu.UserId == userId && cu.CommentId == commentId);
            commentUserRepository.Delete(commentUser);
            SaveCommentUser();
        }

        public void DeleteCommentUser(int id)
        {
            var commentUser = commentUserRepository.GetById(id);
            commentUserRepository.Delete(commentUser);
            SaveCommentUser();
        }


        public void SaveCommentUser()
        {
            unitOfWork.Commit();
        }

        #endregion
    }
}