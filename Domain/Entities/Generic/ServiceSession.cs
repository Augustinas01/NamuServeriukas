using Domain.Entities.General;

namespace Domain.Entities.Generic
{
    public class ServiceSession
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public DateTime StartTimestamp { get; set; }
        public DateTime? StopTimestamp { get; set; }
        public virtual ICollection<Player>? Players { get; }
        public virtual Service? Service { get; set; }
    }
}
