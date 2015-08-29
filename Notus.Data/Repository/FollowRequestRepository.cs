using Notus.Data.Infrastructure;
using Notus.Model.Models;

namespace Notus.Data.Repository
{
    public class FollowRequestRepository : RepositoryBase<FollowRequest>, IFollowRequestRepository
    {
        public FollowRequestRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IFollowRequestRepository : IRepository<FollowRequest>
    {
    }
}