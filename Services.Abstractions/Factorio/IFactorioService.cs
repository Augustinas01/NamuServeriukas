using Contracts.Factorio;
using Services.Abstractions.Generic;

namespace Services.Abstractions.Factorio
{
    public interface IFactorioService : IPlayerService<FactorioPlayerDto>, ISessionService
    {

    }
}
