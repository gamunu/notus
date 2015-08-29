using Notus.Data.Infrastructure;
using Notus.Model.Models;

namespace Notus.Data.Repository
{
    public class GroupUpdateUserRepository : RepositoryBase<GroupUpdateUser>, IGroupUpdateUserRepository
    {
        public GroupUpdateUserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IGroupUpdateUserRepository : IRepository<GroupUpdateUser>
    {
    }
}