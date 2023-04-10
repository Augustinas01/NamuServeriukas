using Contracts.Generic.Session.Infrastructure;
using Domain.Entities.Generic;

namespace Domain.Repositories.Generic
{
    public interface IServiceSessionRepository
    {
        Task<IEnumerable<SessionDto>> GetAllSessionsAsync();
        Task<SessionDto> GetSessionByIdAsync(int id);
        void InsertSession(SessionDto session);
        Task<SessionDto> GetLastSessionByServiceIdAsync(int serviceId);
        void UpdateSession(SessionDto session);
    }
}
