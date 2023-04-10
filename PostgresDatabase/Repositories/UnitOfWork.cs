using Domain.Repositories;
using PostgresDatabase.Contexts;

namespace PostgresDatabase.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly PostgresDbContext _factorioContext;
        
        public UnitOfWork(PostgresDbContext factorioContext) => _factorioContext = factorioContext;
        public Task<int> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync() => _factorioContext.SaveChangesAsync();
    }
}
