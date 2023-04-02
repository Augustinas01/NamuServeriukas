using Enums;
using Services.Abstractions.Facades;
using System.Diagnostics;

namespace Services
{
    internal class ProcessService : IProcessService
    {
        private readonly IProcessManager _processManager;
        public ProcessService(IProcessManager processManager)
        {
            _processManager = processManager;
        }
        public Task StartProcess(ProcessEnum.Type processType)
        {
            _processManager.StartExternalProcess(processType);
            return Task.CompletedTask;
        }

        public Task StopProcess(ProcessEnum.Type processType)
        {
            _processManager.StopExternalProcess(processType);
            return Task.CompletedTask;
        }
    }
}
