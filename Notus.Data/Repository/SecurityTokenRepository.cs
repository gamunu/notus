using Notus.Data.Infrastructure;
using Notus.Model.Models;

namespace Notus.Data.Repository
{
    public class SecurityTokenRepository : RepositoryBase<SecurityToken>, ISecurityTokenRepository
    {
        public SecurityTokenRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }

    public interface ISecurityTokenRepository : IRepository<SecurityToken>
    {
    }
}