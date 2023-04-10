
using Contracts.Generic.Session.Infrastructure;
using Contracts.Generic.User;
using Domain.Entities.Generic;

namespace PostgresDatabase.Mapping
{
    public static class PlayerMapper
    {
        public static PlayerDto MapToDto(Player player)
        {
            return new PlayerDto()
            {
                Id = player.Id,
                Name = player.Name,
                JoinTimestamp = player.JoinTimestamp,
                LeaveTimestamp = player.LeaveTimestamp ?? DateTime.MinValue,
                SessionId = player.SessionId,
            };
        }

        public static Player MapToEntity (PlayerDto player)
        {
            return new Player()
            {
                Id = player.Id,
                Name = player.Name,
                JoinTimestamp = player.JoinTimestamp,
                LeaveTimestamp = player.LeaveTimestamp,
                SessionId = player.SessionId
            };
        }
    }
}

