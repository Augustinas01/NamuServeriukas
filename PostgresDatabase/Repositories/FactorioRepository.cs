using Domain.Entities.Factorio;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using PostgresDatabase.Contexts;

namespace PostgresDatabase.Repositories
{
    internal class FactorioRepository : IFactorioRepository
    {
        private readonly FactorioDbContext _context;

        public FactorioRepository(FactorioDbContext context) => _context = context;

#region Player
        public Task<IEnumerable<FactorioPlayer>> GetAllPlayersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FactorioPlayer>> GetAllPlayersBySessionIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<FactorioPlayer> GetPlayerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlayer(FactorioPlayer entity)
        {
            throw new NotImplementedException();
        }

        public void InsertPlayer(FactorioPlayer entity)
        {
            throw new NotImplementedException();
        }

        #endregion

#region Session
        public Task<IEnumerable<FactorioSession>> GetAllSessionsAsync()
        {
            throw new NotImplementedException();
        }

        public void InsertSession(FactorioSession session) =>    
            _context.Sessions.Add(session);
        public async Task<FactorioSession> GetSessionByIdAsync(int id) =>
            await _context.Sessions.SingleAsync(s => s.Id == id);
        

#endregion

    }
}
