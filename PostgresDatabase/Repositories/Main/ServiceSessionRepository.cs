using Contracts.Generic.Session.Infrastructure;
using Domain.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using PostgresDatabase.Contexts;
using PostgresDatabase.Mapping;

namespace PostgresDatabase.Repositories.Main
{
    internal class ServiceSessionRepository : IServiceSessionRepository
    {
        private readonly PostgresDbContext _dbContext;

        public ServiceSessionRepository(PostgresDbContext ctx) => _dbContext = ctx;

        public Task<IEnumerable<SessionDto>> GetAllSessionsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<SessionDto> GetSessionByIdAsync(int id)
        {
            return ServiceSessionMapper.Map(await _dbContext.Sessions.SingleAsync(s => s.Id == id));
        }

        public void InsertSession(SessionDto session)
        {
            _dbContext.Sessions.Add(ServiceSessionMapper.MapToEntity(session));
        }

        public async Task<SessionDto> GetLastSessionByServiceIdAsync(int serviceId)
        {
            return ServiceSessionMapper.Map(await _dbContext.Sessions
                .OrderBy(s => s.Id)
                .LastAsync(s => s.ServiceId == serviceId));
        }

        public void UpdateSession(SessionDto session)
        {
            var entity = _dbContext.Sessions.Single(s => s.Id == session.Id);
            entity.StopTimestamp = session.StopTimestamp;
        }
    }
}
