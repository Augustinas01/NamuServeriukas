using Enums;
using System.Diagnostics;


namespace Services.Abstractions.Generic
{
    public interface IProcessOutputHandler
    {
        void ConsumeProcessOutput(object s, DataReceivedEventArgs e);
        ProcessEnum.Type DeterminateProcessType(object o);
    }
}
