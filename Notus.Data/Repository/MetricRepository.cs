using Notus.Data.Infrastructure;
using Notus.Model.Models;

namespace Notus.Data.Repository
{
    public class MetricRepository : RepositoryBase<Metric>, IMetricRepository
    {
        public MetricRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface IMetricRepository : IRepository<Metric>
    {
    }
}