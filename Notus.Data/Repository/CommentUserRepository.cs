using Notus.Data.Infrastructure;
using Notus.Model.Models;

namespace Notus.Data.Repository
{
    public class CommentUserRepository : RepositoryBase<CommentUser>, ICommentUserRepository
    {
        public CommentUserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ICommentUserRepository : IRepository<CommentUser>
    {
    }
}