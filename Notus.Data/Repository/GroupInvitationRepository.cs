using Notus.Data.Infrastructure;
using Notus.Model.Models;

namespace Notus.Data.Repository
{
    public class GroupInvitationRepository : RepositoryBase<GroupInvitation>, IGroupInvitationRepository
    {
        public GroupInvitationRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IGroupInvitationRepository : IRepository<GroupInvitation>
    {
    }
}