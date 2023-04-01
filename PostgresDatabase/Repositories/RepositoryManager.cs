using Domain.Entities.Generic;
using Domain.Repositories;
using Domain.Repositories.General;
using PostgresDatabase.Contexts;

namespace PostgresDatabase.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly Lazy<IFactorioRepository> _factorioRepository;
        private readonly Lazy<IGameSessionRepository<GameSession,Player>> _gameSessionRepository;
        private readonly Lazy<IUnitOfWork> _unitOfWork;

        public RepositoryManager(FactorioDbContext dbContext)
        {
            _factorioRepository = new Lazy<IFactorioRepository>(() => new FactorioRepository(dbContext));
            _gameSessionRepository = new Lazy<IGameSessionRepository<GameSession, Player>>(() => new GameSessionRepository(dbContext));
            _unitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));

        }
        public IFactorioRepository FactorioRepository => _factorioRepository.Value;

        public IGameSessionRepository<GameSession, Player> GameSessionRepository => _gameSessionRepository.Value;

        public IUnitOfWork UnitOfWork => _unitOfWork.Value;
    }
}
