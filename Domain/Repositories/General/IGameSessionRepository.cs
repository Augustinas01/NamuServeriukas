using Domain.Entities.Generic;

namespace Domain.Repositories.General
{
    public interface IGameSessionRepository<Gs, Pl> 
        where Gs : class
        where Pl : class
    {
        Task<IEnumerable<Gs>> GetAllSessionsAsync();
        Task<Gs> GetSessionByIdAsync(int id);
        Task<IEnumerable<Pl>> GetAllPlayersBySessionIdAsync(int id);
        void InsertSession(Gs session);
    }
}
