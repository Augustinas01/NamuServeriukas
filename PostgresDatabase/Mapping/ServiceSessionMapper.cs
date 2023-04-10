
using Contracts.Generic.Session.Infrastructure;
using Contracts.Generic.User;
using Domain.Entities.Generic;

namespace PostgresDatabase.Mapping
{
    public static class ServiceSessionMapper
    {
        public static SessionDto Map(ServiceSession session)
        {
            var sessionDto = MapWithoutPlayers(session);

            if (session.Players != null && session.Players.Any())
            {
                sessionDto.Players = (ICollection<PlayerDto>)session.Players.ToList();
            }

            return sessionDto;

        }
        public static ServiceSession MapToEntity(SessionDto session)
        {
            return new ServiceSession()
            {
                Id = session.Id,
                StartTimestamp = session.StartTimestamp,
                StopTimestamp = session.StopTimestamp,
                ServiceId = session.ServiceId
            };
        }
        public static SessionDto MapWithoutPlayers(ServiceSession session)
        {
            return new SessionDto()
            {
                Id = session.Id,
                ServiceId = session.ServiceId,
                StartTimestamp = session.StartTimestamp,
                StopTimestamp = session.StopTimestamp
            };
        }


    }
}
