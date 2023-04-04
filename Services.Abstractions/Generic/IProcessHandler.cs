using Enums;
using Microsoft.Extensions.Hosting;

namespace Services.Abstractions.Generic
{
    public interface IProcessHandler 
    {
        Task StartExternalProcess(ProcessEnum.Type processType);
        Task<int> StopExternalProcess(ProcessEnum.Type processType);
    }
}
