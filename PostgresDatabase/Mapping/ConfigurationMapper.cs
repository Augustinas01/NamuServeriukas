using Contracts.Configuration.Infrastructure;
using Domain.Entities.General;

namespace PostgresDatabase.Mapping
{
    public static class ConfigurationMapper
    {
        public static ConfigurationDto Map(Configuration configuration)
        {
            return new ConfigurationDto()
            {
                Key = configuration.Key,
                Value = configuration.Value
            };
        }
    }
}
