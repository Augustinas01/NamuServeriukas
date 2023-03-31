
namespace Contracts.Generic.Session
{
    public class SessionDto<T>
    {
        public int Id { get; set; }

        public DateTime StartTimestamp { get; set; }

        public DateTime? StopTimestamp { get; set; }

        public ICollection<T> Players { get; set; } = new List<T>();
    }
}
