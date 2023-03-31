using Domain.Entities.Factorio;
using Domain.Repositories.General;

namespace Domain.Repositories
{
    public interface IFactorioRepository : IPlayerRepository<FactorioPlayer>
    {
    }
}
