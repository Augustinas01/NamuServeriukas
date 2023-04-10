
using Domain.Repositories;
using Services.Abstractions;
using Services.Abstractions.Facades;
using Services.Abstractions.Generic;
using Services.ExternalServices;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IExternalServicesManager> _lazyExternalServicesManager;
        private readonly Lazy<IExternalServicesLibrary> _lazyExternalServicesLibrary;

        public ServiceManager(IRepositoryManager repositoryManager, IProcessHandler processService)
        {
            _lazyExternalServicesManager = new Lazy<IExternalServicesManager>(() => new ExternalServicesManager(repositoryManager, processService));
            _lazyExternalServicesLibrary = new Lazy<IExternalServicesLibrary>(() => new ExternalServicesLibrary(repositoryManager));
        }

        public IExternalServicesManager ExternalServicesManager => _lazyExternalServicesManager.Value;
        public IExternalServicesLibrary ExternalServicesLibrary => _lazyExternalServicesLibrary.Value;
    }
}
