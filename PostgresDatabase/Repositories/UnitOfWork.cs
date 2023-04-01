using Domain.Repositories;
using PostgresDatabase.Contexts;

namespace PostgresDatabase.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly FactorioDbContext _factorioContext;
        
        public UnitOfWork(FactorioDbContext factorioContext) => _factorioContext = factorioContext;
        public Task<int> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync() => _factorioContext.SaveChangesAsync();
    }
}
