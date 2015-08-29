using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class GoalStatusConfiguration : EntityTypeConfiguration<GoalStatus>
    {
        public GoalStatusConfiguration()
        {
            Property(g => g.GoalStatusType).HasMaxLength(50);
        }
    }
}