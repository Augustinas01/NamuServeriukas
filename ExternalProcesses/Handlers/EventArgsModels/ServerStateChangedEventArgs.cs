using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalProcesses.Handlers.EventArgsModels
{
    internal class ServerStateChangedEventArgs : EventArgs
    {
        public string? State { get; set; }
        public DateTime Time { get; set; }
    }
}
