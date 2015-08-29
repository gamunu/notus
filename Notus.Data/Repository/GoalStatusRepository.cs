using Notus.Data.Infrastructure;
using Notus.Model.Models;

namespace Notus.Data.Repository
{
    public class GoalStatusRepository : RepositoryBase<GoalStatus>, IGoalStatusRepository
    {
        public GoalStatusRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IGoalStatusRepository : IRepository<GoalStatus>
    {
    }
}