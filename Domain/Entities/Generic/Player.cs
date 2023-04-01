namespace Domain.Entities.Generic
{
    public class Player
    {
        public int Id { get; set; }

        public DateTime JoinTimestamp { get; set; }

        public DateTime? LeaveTimestamp { get; set; }
        public int? SessionId { get; set; }
        public virtual GameSession? Session { get; set; }
    }
}
