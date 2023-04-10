using Contracts.Configuration.Infrastructure;

namespace Domain.Repositories.General
{
    public interface IConfigurationRepository
    {
        Task<ConfigurationDto> GetByKey(string key);
    }
}
