using System.Data.Entity.ModelConfiguration;
using Notus.Model.Models;

namespace Notus.Data.Configuration
{
    public class FollowUserConfiguration : EntityTypeConfiguration<FollowUser>
    {
        public FollowUserConfiguration()
        {
            Property(f => f.Accepted).IsRequired();
            Property(f => f.AddedDate).IsRequired();
        }
    }
}