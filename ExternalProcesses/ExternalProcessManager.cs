using Enums;
using ExternalProcesses.Models;
using Services.Abstractions.Facades;
using System.Diagnostics;

namespace ExternalProcesses
{
    public class ExternalProcessManager : IProcessManager
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
                       StartExternalProcess
                            ("D:\\grajokas\\Factorio.v1.1.76\\Factorio.v1.1.76\\Factorio.v1.1.76\\bin\\x64\\factorio.exe",
                           "--start-server-load-latest --server-settings C:\\server\\games\\Factorio\\data\\server-settings.json");
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
            prc.StartInfo = startInfo;
            prc.Start();
            prc.BeginOutputReadLine();

            _processes.Add(ProcessEnum.Type.Factorio, prc);

            return Task.CompletedTask;
        }

        public async Task<int> StopExternalProcess(ProcessEnum.Type prcType)
        {
            var prcId = _processes[prcType].GetServerId();
            await _processes[prcType].WaitForExitAsync();

            _processes[prcType].Kill();
            _processes[prcType].Dispose();
            _processes.Remove(prcType);

            return prcId;
        }

        public void SetGameId(ProcessEnum.Type prcType, int id)
        {
            _processes[prcType].SetServerId(id);
        }



    }
}
