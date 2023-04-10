using Contracts.Configuration;
using Domain.Entities.Generic;
using Domain.Repositories;
using Domain.Repositories.General;
using Domain.Repositories.Generic;
using PostgresDatabase.Contexts;
using PostgresDatabase.Repositories.Config;
using PostgresDatabase.Repositories.Main;

namespace PostgresDatabase.Repositories
{
    public class RepositoryManager : IRepositoryManager
    { 
        private readonly Lazy<IServiceSessionRepository> _gameSessionRepository;
        private readonly Lazy<IConfigurationRepository> _configurationRepository;
        private readonly Lazy<IServiceRepository> _serviceRepository;
        private readonly Lazy<IPlayerRepository> _playerRepository;
        private readonly Lazy<IUnitOfWork> _unitOfWork;

        public RepositoryManager(PostgresDbContext dbContext)
        {
            _gameSessionRepository = new Lazy<IServiceSessionRepository>(() => new ServiceSessionRepository(dbContext));
            _configurationRepository = new Lazy<IConfigurationRepository>(() => new ConfigurationRepository(dbContext));
            _serviceRepository = new Lazy<IServiceRepository>(() => new ServiceRepository(dbContext));
            _playerRepository = new Lazy<IPlayerRepository>(() => new PlayerRepository(dbContext));
            _unitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(dbContext));

        }

        public IServiceSessionRepository ServiceSessionRepository => _gameSessionRepository.Value;
        public IConfigurationRepository ConfigurationRepository => _configurationRepository.Value;
        public IServiceRepository ServiceRepository => _serviceRepository.Value;
        public IPlayerRepository PlayerRepository => _playerRepository.Value;
        public IUnitOfWork UnitOfWork => _unitOfWork.Value;


    }
}
