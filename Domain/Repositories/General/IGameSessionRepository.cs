using Domain.Entities.General;

namespace Domain.Repositories.General
{
    public interface IGameSessionRepository
    {
        Task<IEnumerable<GameSession>> GetAllSessionsAsync();
        Task<GameSession> GetSessionByIdAsync(int id);
        Task<IEnumerable<Player>> GetAllPlayersBySessionIdAsync(int id);
        void Insert(GameSession session);
    }
}
