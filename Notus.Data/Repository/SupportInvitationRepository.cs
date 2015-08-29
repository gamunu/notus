using Notus.Data.Infrastructure;
using Notus.Model.Models;

namespace Notus.Data.Repository
{
    public class SupportInvitationRepository : RepositoryBase<SupportInvitation>, ISupportInvitationRepository
    {
        public SupportInvitationRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ISupportInvitationRepository : IRepository<SupportInvitation>
    {
    }
}