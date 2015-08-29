using Notus.Data.Infrastructure;
using Notus.Model.Models;

namespace Notus.Data.Repository
{
    public class GroupRequestRepository : RepositoryBase<GroupRequest>, IGroupRequestRepository
    {
        public GroupRequestRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IGroupRequestRepository : IRepository<GroupRequest>
    {
    }
}