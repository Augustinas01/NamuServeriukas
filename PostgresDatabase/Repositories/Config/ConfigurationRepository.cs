using Contracts.Configuration.Infrastructure;
using Domain.Entities.General;
using Domain.Repositories.General;
using Microsoft.EntityFrameworkCore;
using PostgresDatabase.Contexts;
using PostgresDatabase.Mapping;

namespace PostgresDatabase.Repositories.Config
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly PostgresDbContext _dbContext;
        public ConfigurationRepository(PostgresDbContext ctx) => _dbContext = ctx;

        public async Task<ConfigurationDto> GetByKey(string key)
        {
            var c = await _dbContext.Configurations.SingleAsync(e => e.Key == key);
            return ConfigurationMapper.Map(c);
        }
    }
}
