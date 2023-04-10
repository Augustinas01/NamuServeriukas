using Contracts.Generic.User;
using Domain.Entities.Generic;

namespace Domain.Repositories.Generic
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<PlayerDto>> GetAllPlayersAsync();
        Task<PlayerDto> GetPlayerByIdAsync(int id);
        Task<List<PlayerDto>> GetAllPLayersBySessionId(int id);
        void InsertPlayer(PlayerDto player);
        void UpdatePlayer(PlayerDto player);
    }
}
