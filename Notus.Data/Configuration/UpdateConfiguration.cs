using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class UpdateConfiguration : EntityTypeConfiguration<Update>
    {
        public UpdateConfiguration()
        {
            Property(u => u.Updatemsg).IsMaxLength();
            Property(u => u.GoalId).IsRequired();
            Property(u => u.UpdateDate).IsRequired();
        }
    }
}