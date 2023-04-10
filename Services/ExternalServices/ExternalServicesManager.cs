
using Contracts.Generic.Service;
using Contracts.Generic.Session.Infrastructure;
using Domain.Entities.Generic;
using Domain.Repositories;
using Services.Abstractions.Facades;
using Services.Abstractions.Generic;
using Services.Mapping;

namespace Services.ExternalServices
{
    internal class ExternalServicesManager : IExternalServicesManager
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IProcessHandler _processHandler;
        public ExternalServicesManager(
            IRepositoryManager repositoryManager,
            IProcessHandler processHandler)
        {
            _repositoryManager = repositoryManager;
            _processHandler = processHandler;
        }
        public async Task<int> Start(int id)
        {
            var service = await _repositoryManager.ServiceRepository.GetById(id);

            await _processHandler.StartExternalProcess(ServiceMapper.MapForLaunch(service));

            var session = new SessionDto()
            {
                ServiceId = service.Id,
                StartTimestamp = DateTime.UtcNow
            };
            _repositoryManager.ServiceSessionRepository.InsertSession(session);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            return session.Id;
        }

        public async Task Stop(int id)
        {
            var session = await _repositoryManager.ServiceSessionRepository.GetLastSessionByServiceIdAsync(id);

            _processHandler.StopExternalProcess(id);

            session.StopTimestamp = DateTime.UtcNow;
            _repositoryManager.ServiceSessionRepository.UpdateSession(session);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public ServiceModel GetServiceInfo(int serviceId)
        {
            return _processHandler.GetServiceModel(serviceId);
        }

    }
}
