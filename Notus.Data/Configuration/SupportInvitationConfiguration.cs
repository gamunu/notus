using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class SupportInvitationConfiguration : EntityTypeConfiguration<SupportInvitation>
    {
        public SupportInvitationConfiguration()
        {
            Property(s => s.GoalId).IsRequired();
            Property(s => s.SentDate).IsRequired();
            Property(s => s.Accepted).IsRequired();
            Property(s => s.FromUserId).IsMaxLength();
            Property(s => s.ToUserId).IsMaxLength();
        }
    }
}