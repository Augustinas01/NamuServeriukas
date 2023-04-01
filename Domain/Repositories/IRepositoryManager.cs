
using Domain.Entities.Generic;
using Domain.Repositories.General;

namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        IFactorioRepository FactorioRepository { get; }
        IGameSessionRepository<GameSession, Player> GameSessionRepository { get; }
        IUnitOfWork UnitOfWork { get; }
    }
}
