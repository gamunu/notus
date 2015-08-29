namespace Notus.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}