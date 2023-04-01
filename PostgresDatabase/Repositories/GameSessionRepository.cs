using Domain.Entities.Generic;
using Domain.Repositories.General;
using Microsoft.EntityFrameworkCore;
using PostgresDatabase.Contexts;

namespace PostgresDatabase.Repositories
{
    internal class GameSessionRepository : IGameSessionRepository<GameSession,Player>
    {
        private readonly FactorioDbContext _factorioDbContext;

        public GameSessionRepository(FactorioDbContext factorioDbContext) => _factorioDbContext = factorioDbContext;
        public Task<IEnumerable<Player>> GetAllPlayersBySessionIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GameSession>> GetAllSessionsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GameSession> GetSessionByIdAsync(int id)
        {
            return await _factorioDbContext.Sessions.SingleAsync(s => s.Id == id );
        }

        public void InsertSession(GameSession session)
        {
            //_factorioDbContext.Sessions.Add(session);
        }
    }
}
