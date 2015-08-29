using Notus.Data.Infrastructure;
using Notus.Data.Repository;
using Notus.Model.Models;

namespace Notus.Service
{
    public interface IUserProfileService
    {
        UserProfile GetProfile(int id);
        UserProfile GetUser(string userid);
        UserProfile GetUserByEmail(string email);

        void CreateUserProfile(string userId);
        void UpdateUserProfile(UserProfile user);
        void SaveUserProfile();
    }

    public class UserProfileService : IUserProfileService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserProfileRepository userProfileRepository;

        public UserProfileService(IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork)
        {
            this.userProfileRepository = userProfileRepository;
            this.unitOfWork = unitOfWork;
        }

        public UserProfile GetProfile(int id)
        {
            var userprofile = userProfileRepository.Get(u => u.UserProfileId == id);
            return userprofile;
        }

        public UserProfile GetUser(string userid)
        {
            var userprofile = userProfileRepository.Get(u => u.UserId == userid);
            return userprofile;
        }

        public UserProfile GetUserByEmail(string email)
        {
            var userProfile = userProfileRepository.Get(u => u.Email == email);
            return userProfile;
        }

        public void CreateUserProfile(string userId)
        {
            var newUserProfile = new UserProfile();
            newUserProfile.UserId = userId;
            userProfileRepository.Add(newUserProfile);
            SaveUserProfile();
        }

        public void UpdateUserProfile(UserProfile user)
        {
            userProfileRepository.Update(user);
            SaveUserProfile();
        }

        public void SaveUserProfile()
        {
            unitOfWork.Commit();
        }
    }
}