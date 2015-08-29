using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class GoalUpdateConfiguration : EntityTypeConfiguration<GoalUpdate>
    {
        public GoalUpdateConfiguration()
        {
            Property(g => g.Updatemsg).HasMaxLength(50);
            Property(g => g.GoalId).IsRequired();
        }
    }
}