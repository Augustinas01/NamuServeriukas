
using Contracts.Configuration.Infrastructure;
using Contracts.Configuration.Presentation;


namespace Services.Mapping
{
    public static class ServiceMapper
    {
        public static ServicePresentationDto MapForPresentation(ServiceDto service)
        {
            return new ServicePresentationDto()
            {
                Id = service.Id,
                Name = service.Name,
                Type = service.Type,
                Description = service.Description,
                IsActive = service.SessionsWithoutPlayers != null && service.SessionsWithoutPlayers.Count > 0,
                StartedDateTimeUtc = service.SessionsWithoutPlayers != null ? service.SessionsWithoutPlayers[0].StartTimestamp : DateTime.MinValue //TODO get Last session maybe?
                //TODO playerList

            };
        }

        public static ServiceLaunchDto MapForLaunch(ServiceDto service)
        {
            return new ServiceLaunchDto()
            {
                Id = service.Id,
                Name = service.Name,
                PathToExe = service.PathToExe,
                ExeArgs = service.ExeArgs
            };
        }
    }
}
