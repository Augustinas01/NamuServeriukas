using Enums;
using ExternalProcesses.Handlers;
using ExternalProcesses.Models;
using Services.Abstractions.Facades;
using System.Diagnostics;

namespace ExternalProcesses
{
    internal class ExternalProcessManager : IProcessManager
    {
        private readonly Dictionary<ProcessEnum.Type, GameServer> _processes = new();

        #region HostedService start/down
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        #endregion

        public Task StartExternalProcess(ProcessEnum.Type processType)
        {
            if (_processes.Values.Any(p => p.Type == processType))
            {
                throw new InvalidOperationException("A process is already running.");
            }

            switch (processType)
            {
                case ProcessEnum.Type.Factorio:
                    try
                    {
                       StartExternalProcess("path", "args");
                    }
                    catch
                    {
                        throw;
                    }
                    break;
                default:
                    throw new ArgumentException("Unknown process type");
            }


            return Task.CompletedTask;
        }
        public Task StartExternalProcess(string filePath, string arguments)
        {

            ProcessStartInfo startInfo = new()
            {
                FileName = $"{filePath}",
                Arguments = $"{arguments}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = false
            };

            var prc = new GameServer();
            prc.Start(startInfo);

            _processes.Add(ProcessEnum.Type.Factorio, prc);

            return Task.CompletedTask;
        }

        public Task StopExternalProcess(ProcessEnum.Type prcType) =>
            StopExternalProcess("path");

        public Task StopExternalProcess(string filePath)
        {
            throw new NotImplementedException();
        }


    }
}
