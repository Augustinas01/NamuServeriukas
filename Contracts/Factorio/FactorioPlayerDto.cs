using Contracts.Generic.Session;

namespace Contracts.Factorio
{
    public class FactorioPlayerDto
    {
        public int Id { get; set; }
        public DateTime JoinTimestamp { get; set; }
        public DateTime? LeaveTimestamp { get; set; }
        public int? SessionId { get; set; }
        public SessionDto<FactorioPlayerDto>? Session { get; set; }
    }
}
