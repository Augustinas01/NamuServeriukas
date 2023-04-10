using Enums;

namespace Contracts.Generic.Session.Infrastructure
{
    public class SessionWithoutPlayersDto
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public ServiceEnum.Name ServiceType { get; set; }
        public DateTime StartTimestamp { get; set; }
        public DateTime? StopTimestamp { get; set; }
    }
}
