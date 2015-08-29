using Notus.Data.Infrastructure;
using Notus.Model.Models;

namespace Notus.Data.Repository
{
    public class FollowUserRepository : RepositoryBase<FollowUser>, IFollowUserRepository
    {
        public FollowUserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        //public IEnumerable<User> SearchUserForGroup(string searchString, int groupId)
        //{

        //}
    }

    public interface IFollowUserRepository : IRepository<FollowUser>
    {
        //IEnumerable<User> SearchUserForGroup(string searchString, int groupId);
    }
}