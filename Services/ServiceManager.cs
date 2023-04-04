
using Domain.Repositories;
using Services.Abstractions;
using Services.Abstractions.Facades;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IFactorioService> _lazyFactorioService;

        public ServiceManager(IRepositoryManager repositoryManager, IProcessService processService)
        {
            _lazyFactorioService = new Lazy<IFactorioService>(() => new FactorioService(repositoryManager, processService));
        }

        public IFactorioService FactorioService => _lazyFactorioService.Value;
    }
}
