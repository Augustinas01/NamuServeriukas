using Contracts.Generic.User;

namespace Contracts.Generic.Session.Infrastructure
{
    public class SessionDto : SessionWithoutPlayersDto
    {
        public ICollection<PlayerDto> Players { get; set; } = new List<PlayerDto>();
    }
}
