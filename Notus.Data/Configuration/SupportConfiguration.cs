using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class SupportConfiguration : EntityTypeConfiguration<Support>
    {
        public SupportConfiguration()
        {
            Property(s => s.SupportedDate).IsRequired();
            Property(s => s.GoalId).IsRequired();
            Property(s => s.UserId).IsMaxLength();
        }
    }
}