using Contracts.Generic.Session.Infrastructure;

namespace Contracts.Generic.User
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime JoinTimestamp { get; set; }
        public DateTime? LeaveTimestamp { get; set; }
        public int SessionId { get; set; }
        public SessionDto? Session { get; set; }
    }
}
