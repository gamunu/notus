using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class GroupInvitationConfiguration : EntityTypeConfiguration<GroupInvitation>
    {
        public GroupInvitationConfiguration()
        {
            Property(g => g.FromUserId).IsMaxLength();
            Property(g => g.ToUserId).IsMaxLength();
            Property(g => g.GroupId).IsRequired();
            Property(g => g.SentDate).IsRequired();
            Property(g => g.Accepted).IsRequired();
        }
    }
}