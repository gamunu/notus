using Notus.Data.Infrastructure;
using Notus.Model.Models;

namespace Notus.Data.Repository
{
    public class GroupUpdateRepository : RepositoryBase<GroupUpdate>, IGroupUpdateRepository
    {
        public GroupUpdateRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IGroupUpdateRepository : IRepository<GroupUpdate>
    {
    }
}