using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class FollowRequestConfiguration : EntityTypeConfiguration<FollowRequest>
    {
        public FollowRequestConfiguration()
        {
            Property(f => f.FromUserId).IsRequired();
            Property(f => f.ToUserId).IsRequired();
            Property(f => f.Accepted).IsRequired();
        }
    }
}