using Contracts;
using Contracts.Configuration.Infrastructure;
using Contracts.Generic.Service;
using Enums;
using ExternalProcesses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Abstractions.Generic;

namespace Services
{
    public class ProcessService : IProcessHandler
    {
        private readonly ExternalProcessManager _processManager;

        public ProcessService(IServiceProvider serviceProvider)
        {
            _processManager = serviceProvider.GetServices<IHostedService>().OfType<ExternalProcessManager>().Single();
        }


        public List<int> GetRunningProcessesIds()
        {
            return _processManager.GetRunningProcessesIds();
        }


        public Task StartExternalProcess(ServiceLaunchDto launchParams)
        {
            _processManager.StartExternalProcess(launchParams);
            return Task.CompletedTask;
        }

        public void StopExternalProcess(int serviceId)
        {
            _processManager.StopExternalProcess(serviceId);
        }

        public ServiceModel GetServiceModel(int serviceId)
        {
            return _processManager.GetServiceModel(serviceId);
        }

    }
}
