using Notus.Data.Infrastructure;
using Notus.Model.Models;

namespace Notus.Data.Repository
{
    internal class GroupUpdateSupportRepository : RepositoryBase<GroupUpdateSupport>, IGroupUpdateSupportRepository
    {
        public GroupUpdateSupportRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IGroupUpdateSupportRepository : IRepository<GroupUpdateSupport>
    {
    }
}