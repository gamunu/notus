namespace Notus.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private NotusEntities _dataContext;

        public NotusEntities Get()
        {
            return _dataContext ?? (_dataContext = new NotusEntities());
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
    }
}