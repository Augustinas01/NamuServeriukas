
using Domain.Repositories.General;

namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        IFactorioRepository FactorioRepository { get; }
        IGameSessionRepository GameSessionRepository { get; }
        IUnitOfWork UnitOfWork { get; }
    }
}
