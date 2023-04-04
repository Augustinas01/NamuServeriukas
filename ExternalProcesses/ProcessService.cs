using Enums;
using ExternalProcesses;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Abstractions.Facades;


namespace Services
{
    public class ProcessService : IProcessService
    {
        private readonly ExternalProcessManager _processManager;
        public ProcessService(IServiceProvider serviceProvider)
        {
            _processManager = serviceProvider.GetServices<IHostedService>().OfType<ExternalProcessManager>().Single(); ;
        }
        public Task StartProcess(ProcessEnum.Type processType)
        {
            _processManager.StartExternalProcess(processType);
            return Task.CompletedTask;
        }

        public async Task<int> StopProcess(ProcessEnum.Type processType)
        {
            var val = await _processManager.StopExternalProcess(processType);
            return val;
        }

        public void SetServerId(ProcessEnum.Type t, int id)
        {
            _processManager.SetGameId(t, id);
        }
    }
}
