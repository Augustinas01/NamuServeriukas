using Contracts;
using Contracts.Configuration.Infrastructure;
using Contracts.Generic.Service;

namespace Services.Abstractions.Generic
{
    public interface IProcessHandler 
    {
        Task StartExternalProcess(ServiceLaunchDto processType);
        void StopExternalProcess(int serviceId);
        List<int> GetRunningProcessesIds();
        ServiceModel GetServiceModel(int id);
    }
}
