using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class MetricConfiguration : EntityTypeConfiguration<Metric>
    {
        public MetricConfiguration()
        {
            Property(m => m.Type).IsMaxLength();
        }
    }
}