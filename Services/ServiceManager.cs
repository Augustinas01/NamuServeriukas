
using Domain.Repositories;
using Services.Abstractions;
using Services.Abstractions.Factorio;

namespace Services
{
    internal class ServiceManager : IServiceManager
    {
        private readonly Lazy<IFactorioService> _lazyFactorioService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _lazyFactorioService = new Lazy<IFactorioService>(() => new FactorioService(repositoryManager));
        }

        public IFactorioService FactorioService => _lazyFactorioService.Value;
    }
}
