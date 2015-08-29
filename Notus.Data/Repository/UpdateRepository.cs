using Notus.Data.Infrastructure;
using Notus.Model.Models;

namespace Notus.Data.Repository
{
    public class UpdateRepository : RepositoryBase<Update>, IUpdateRepository
    {
        public UpdateRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IUpdateRepository : IRepository<Update>
    {
    }
}