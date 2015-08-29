using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class FocusConfiguration : EntityTypeConfiguration<Focus>
    {
        public FocusConfiguration()
        {
            Property(f => f.FocusName).HasMaxLength(50);
            Property(f => f.Description).HasMaxLength(100);
        }
    }
}