using Enums;

namespace Services.Abstractions.Facades
{
    public interface IProcessService
    {
        Task StartProcess(ProcessEnum.Type processType);
        Task StopProcess(ProcessEnum.Type processType);
    }
}
