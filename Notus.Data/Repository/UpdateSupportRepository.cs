using Notus.Data.Infrastructure;
using Notus.Model.Models;

namespace Notus.Data.Repository
{
    internal class UpdateSupportRepository : RepositoryBase<UpdateSupport>, IUpdateSupportRepository
    {
        public UpdateSupportRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IUpdateSupportRepository : IRepository<UpdateSupport>
    {
    }
}