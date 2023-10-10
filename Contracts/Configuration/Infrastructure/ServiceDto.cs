using Contracts.Generic.Service;
using Contracts.Generic.Session.Infrastructure;
using Enums;

namespace Contracts.Configuration.Infrastructure
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public ServiceEnum.Type Type { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PathToExe { get; set; }
        public string? ExeArgs { get; set; }
        public List<ServicePort>? PortList { get; set; }
        public List<SessionDto>? SessionsWithoutPlayers { get; set; }

    }
}
