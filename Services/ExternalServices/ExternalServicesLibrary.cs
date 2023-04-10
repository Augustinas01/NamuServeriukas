using Contracts.Configuration.Infrastructure;
using Domain.Repositories;
using Enums;
using Services.Abstractions.Facades;

namespace Services.ExternalServices
{
    public class ExternalServicesLibrary : IExternalServicesLibrary
    {
        private readonly IRepositoryManager _repositoryManager;
        public ExternalServicesLibrary(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public List<ServiceDto> GetAvailableServices()
        {
            var localServices = GetAvailableDirectoriesLocal()
                                    .Select(d => d.Substring(d.LastIndexOf('\\') + 1))
                                    .ToList();
            var configuredServices = GetConfiguredServices().Result;

            var availableService = configuredServices
                .Where(configured => configured.Name != null && localServices.Contains(configured.Name))
                .ToList();

            return availableService;

        }
        public string[] GetAvailableDirectoriesLocal()
        {
            var dir = GetGamesDirectory().Result;
            var dirs = Directory.GetDirectories(dir);
            return dirs;
        }

        private async Task<string> GetGamesDirectory()
        {
#if DEBUG
            var cfg = await _repositoryManager.ConfigurationRepository.GetByKey(ConfigurationKeysEnum.DEVGamesDirectoryPath);
#else
            var cfg =  await _repositoryManager.ConfigurationRepository.GetByKey(ConfigurationKeysEnum.GamesDirectoryPath);
#endif
            return cfg.Value;
        }

        public async Task<List<ServiceDto>> GetConfiguredServices()
        {
            var services = await _repositoryManager.ServiceRepository.GetAllAsync();

            return services;
        }
    }
}
