using Notus.Data.Infrastructure;
using Notus.Model.Models;

namespace Notus.Data.Repository
{
    public class GroupCommentUserRepository : RepositoryBase<GroupCommentUser>, IGroupCommentUserRepository
    {
        public GroupCommentUserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IGroupCommentUserRepository : IRepository<GroupCommentUser>
    {
    }
}