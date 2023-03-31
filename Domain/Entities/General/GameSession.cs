namespace Domain.Entities.General
{
    public class GameSession
    {
        public int Id { get; set; }

        public DateTime StartTimestamp { get; set; }

        public DateTime? StopTimestamp { get; set; }

        public virtual ICollection<Player> Players { get; } = new List<Player>();
    }
}
