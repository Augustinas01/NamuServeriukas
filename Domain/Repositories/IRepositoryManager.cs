
using Domain.Entities.Generic;
using Domain.Repositories.General;
using Domain.Repositories.Generic;

namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        IServiceSessionRepository ServiceSessionRepository { get; }
        IConfigurationRepository ConfigurationRepository { get; }
        IPlayerRepository PlayerRepository { get; }
        IServiceRepository ServiceRepository { get; }
        IUnitOfWork UnitOfWork { get; }
    }
}
