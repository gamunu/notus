using Notus.Data.Infrastructure;
using Notus.Model.Models;

namespace Notus.Data.Repository
{
    internal class SupportRepository : RepositoryBase<Support>, ISupportRepository
    {
        public SupportRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        //public IEnumerable<Goal> GetUserSupportedGoals(int userid)
        //{
        //    var goals = (from g in this.DataContext.Goals where (from s in this.DataContext.Support where s.UserId == userid select s.GoalId).ToList().Contains(g.GoalId) select g).ToList();

        //    return goals;
        //    //GetAll()select g.Where(s => s.UserId == userid).Join(goalService.GetGoals(), s => s.GoalId, g => g.GoalId, (s, g) => g);
        //}
    }

    public interface ISupportRepository : IRepository<Support>
    {
        //IEnumerable<Goal> GetUserSupportedGoals(int userid);
    }
}