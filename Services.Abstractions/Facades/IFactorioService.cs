using Contracts.Factorio;
using Services.Abstractions.Generic;

namespace Services.Abstractions.Facades

{
    public interface IFactorioService : IPlayerService<FactorioPlayerDto>, ISessionService
    {

    }
}
