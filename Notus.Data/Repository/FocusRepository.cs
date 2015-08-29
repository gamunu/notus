using Notus.Data.Infrastructure;
using Notus.Model.Models;

namespace Notus.Data.Repository
{
    public class FocusRepository : RepositoryBase<Focus>, IFocusRepository
    {
        public FocusRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IFocusRepository : IRepository<Focus>
    {
    }
}