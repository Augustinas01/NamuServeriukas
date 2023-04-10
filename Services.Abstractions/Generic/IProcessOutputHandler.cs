using Enums;
using System.Diagnostics;


namespace Services.Abstractions.Generic
{
    public interface IProcessOutputHandler
    {
        void ConsumeProcessOutput(object s, DataReceivedEventArgs e);
        ServiceEnum.Name DeterminateProcessType(object o);
    }
}
