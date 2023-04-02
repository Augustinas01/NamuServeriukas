using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalProcesses.Handlers.EventArgsModels
{
    internal class PlayerActionEventArgs : EventArgs
    {
        public string? Action { get; set; }
        public string? PlayerName { get; set; }
        public DateTime? Time { get; set; }

    }
}
