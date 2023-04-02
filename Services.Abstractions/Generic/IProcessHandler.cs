using Enums;
using Microsoft.Extensions.Hosting;

namespace Services.Abstractions.Generic
{
    public interface IProcessHandler : IHostedService
    {
        Task StartExternalProcess(ProcessEnum.Type processType);
        Task StopExternalProcess(ProcessEnum.Type processType);
    }
}
