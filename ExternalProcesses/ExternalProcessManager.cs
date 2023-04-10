using Contracts;
using Contracts.Configuration.Infrastructure;
using Contracts.Generic.Service;
using Contracts.Generic.User;
using Domain.Repositories;
using Enums;
using ExternalProcesses.Models;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions.Facades;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

namespace ExternalProcesses
{
    public class ExternalProcessManager : IProcessManager
    {

        private readonly Dictionary<int, GameServer> _processes = new();
        private readonly IServiceProvider _serviceProvider;

        public ExternalProcessManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


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

        public Task StartExternalProcess(ServiceLaunchDto ServiceLaunchParams)
        {
            if (_processes.Values.Any(p => p.Id == ServiceLaunchParams.Id))
            {
                throw new InvalidOperationException("A process is already running.");
            }

            if (ServiceLaunchParams.PathToExe != null && ServiceLaunchParams.ExeArgs != null)
            {

                var prc = new GameServer(ServiceLaunchParams);
                prc.Start();
                prc.BeginOutputReadLine();
                prc.PlayerAction += OnServerAction;

                _processes.Add(ServiceLaunchParams.Id, prc);

            }
            return Task.CompletedTask;
        }

        public void StopExternalProcess(int serviceId)
        {
            _processes[serviceId].Kill();
            _processes[serviceId].Dispose();
            _processes.Remove(serviceId);

        }

        public ServiceModel GetServiceModel(int serviceId)
        {
            return _processes[serviceId].Model;
        }


        public List<int> GetRunningProcessesIds()
        {
            return _processes.Keys.ToList();
        }

        private async void OnServerAction(object? sender, ExternalServiceArgs e)
        {
            switch (e.Action)
            {
                case "join": 
                    using(IServiceScope scope = _serviceProvider.CreateScope())
                    {
                        IRepositoryManager manager = scope.ServiceProvider.GetRequiredService<IRepositoryManager>();
                        var lastSession = await manager.ServiceSessionRepository.GetLastSessionByServiceIdAsync(1);
                        manager.PlayerRepository.InsertPlayer(new PlayerDto()
                        {
                            Name = e.PlayerName,
                            JoinTimestamp = e.Time,
                            SessionId = lastSession.Id
                        });
                        manager.UnitOfWork.SaveChangesAsync().Wait();
                    }
                    break;
                case "leave":
                    using (IServiceScope scope = _serviceProvider.CreateScope())
                    {
                        IRepositoryManager manager = scope.ServiceProvider.GetRequiredService<IRepositoryManager>();
                        var lastSession = await manager.ServiceSessionRepository.GetLastSessionByServiceIdAsync(1);
                        var player = manager.PlayerRepository.GetAllPLayersBySessionId(lastSession.Id).Result.Single( p =>  p.Name != null && p.Name.Equals(e.PlayerName));
                        player.LeaveTimestamp = e.Time;
                        manager.PlayerRepository.UpdatePlayer(player);
                        manager.UnitOfWork.SaveChangesAsync().Wait();
                    }
                    break;
                
            }
        }
    }
}
