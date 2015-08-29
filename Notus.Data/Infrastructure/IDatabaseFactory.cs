using System;

namespace Notus.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        NotusEntities Get();
    }
}