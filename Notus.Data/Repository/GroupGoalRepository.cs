using Notus.Data.Infrastructure;
using Notus.Model.Models;

namespace Notus.Data.Repository
{
    public class GroupGoalRepository : RepositoryBase<GroupGoal>, IGroupGoalRepository
    {
        public GroupGoalRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IGroupGoalRepository : IRepository<GroupGoal>
    {
    }
}