using Enums;

namespace Services.Abstractions.Facades
{
    public interface IProcessService
    {
        Task StartProcess(ProcessEnum.Type processType);
        Task<int> StopProcess(ProcessEnum.Type processType);
        void SetServerId(ProcessEnum.Type t, int id);
    }
}
