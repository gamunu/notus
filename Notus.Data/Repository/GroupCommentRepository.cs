using Notus.Data.Infrastructure;
using Notus.Model.Models;

namespace Notus.Data.Repository
{
    public class GroupCommentRepository : RepositoryBase<GroupComment>, IGroupCommentRepository
    {
        public GroupCommentRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IGroupCommentRepository : IRepository<GroupComment>
    {
    }
}