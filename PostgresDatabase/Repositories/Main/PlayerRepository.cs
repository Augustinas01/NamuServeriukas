
using Contracts.Generic.User;
using Domain.Repositories.Generic;
using PostgresDatabase.Contexts;
using PostgresDatabase.Mapping;

namespace PostgresDatabase.Repositories.Main
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly PostgresDbContext _dbContext;

        public PlayerRepository(PostgresDbContext dbContext) => _dbContext = dbContext;

        public Task<IEnumerable<PlayerDto>> GetAllPlayersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<PlayerDto>> GetAllPLayersBySessionId(int id)
        {
            var pl = _dbContext.Players.Where(e => e.SessionId == id);
            return Task.FromResult(pl.Select(p => PlayerMapper.MapToDto(p)).ToList());
        }

        public Task<PlayerDto> GetPlayerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertPlayer(PlayerDto player)
        {
            _dbContext.Players.Add(PlayerMapper.MapToEntity(player));
        }

        public void UpdatePlayer(PlayerDto player)
        {
            var entity = _dbContext.Players.Single(p => p.Id == player.Id);
            entity.LeaveTimestamp = player.LeaveTimestamp;
        }
    }
}
