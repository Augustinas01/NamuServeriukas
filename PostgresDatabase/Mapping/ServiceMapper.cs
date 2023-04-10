using Contracts.Configuration.Infrastructure;
using Contracts.Generic.Session.Infrastructure;
using Domain.Entities.General;
using Domain.Entities.Generic;
using Enums;

namespace PostgresDatabase.Mapping
{
    public static class ServiceMapper
    {
        public static ServiceDto Map(Service service)
        {
            if (service.Type != null)
            {
                return new ServiceDto()
                {
                    Id = service.Id,
                    Type = GetTypeEnum(service.Type),
                    Name = service.Name,
                    Description = service.Description,
                    PathToExe = service.PathToExe,
                    ExeArgs = service.ExeArgs,
                    SessionsWithoutPlayers = GetSessionWithoutPlayersList(service),
                };
            }
            throw new ArgumentException("Mapping Service without type");
        }

        private static ServiceEnum.Type GetTypeEnum(string type)
        {
            return type switch
            {
                "game" => ServiceEnum.Type.Game,
                "software" => ServiceEnum.Type.Software,
                _ => ServiceEnum.Type.Undefined
            };
        }

        private static List<SessionDto> GetSessionWithoutPlayersList(Service s)
        {
            var list = new List<SessionDto>();

            if (s.Sessions != null && s.Sessions.Any()) 
            {
                 list = s.Sessions.ToList().Select(s => ServiceSessionMapper.MapWithoutPlayers(s)).ToList();
            }
            return list;
        }
            
    }
}
