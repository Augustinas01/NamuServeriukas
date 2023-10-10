
using Contracts.Generic.User;

namespace Contracts.Generic.Service
{
    public class ServiceModel
    {
        public int Id { get; set; }
        public string? State { get; set; }
        public DateTime StartTime { get; set; }
        public List<ServicePort>? PortList { get; set; }
        public List<PlayerDto>? PlayerList { get; set; }

    }
}
