namespace Domain.Entities.Generic
{
    public class GameSession
    {
        public int Id { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Type { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public DateTime StartTimestamp { get; set; }

        public DateTime? StopTimestamp { get; set; }

        //public virtual ICollection<Player> Players { get; } = new List<Player>();
    }
}
