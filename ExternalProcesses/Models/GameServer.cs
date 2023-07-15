
using Contracts;
using Contracts.Configuration.Infrastructure;
using Contracts.Generic.Service;
using Contracts.Generic.User;
using ExternalProcesses.Handlers;
using System.Diagnostics;

namespace ExternalProcesses.Models
{
    public class GameServer : Process
    {
        public ServiceModel Model { get; set; } = new();
        public ProcessOutputHandler ProcessOutputHandler { get; set; }

        public event EventHandler<ExternalServiceArgs> PlayerAction = delegate { };

        public GameServer(ServiceLaunchDto launchParams) 
        {
            base.StartInfo = new()
            {
                FileName = $"{launchParams.PathToExe}",
                Arguments = $"{launchParams.ExeArgs}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };

            Model.PlayerList = new();
            Model.Id = launchParams.Id;
            ProcessOutputHandler = new();
            base.OutputDataReceived += ProcessOutputHandler.ConsumeProcessOutput;
            ProcessOutputHandler.ServerStateChanged += OnServerAction;
            
        }

        private void OnServerAction(object? sender, ExternalServiceArgs args)
        {
            if (args != null)
            {
                switch (args.ServiceAction)
                {
                    case Enums.ServiceEnum.Action.ServiceStateChanged:
                        Model.State = args.State ?? "warming-up";
                        if(args.State != null && args.State.Equals("online") && Model.StartTime == DateTime.MinValue)
                        {
                            Model.StartTime = args.Time;
                        }
                        break;
                    case Enums.ServiceEnum.Action.PlayerAction:
                        switch (args.Action)
                        {
                            case "join":
                                var player = new PlayerDto() 
                                { 
                                    Name = args.PlayerName,
                                    JoinTimestamp = args.Time
                                };
                                Model.PlayerList?.Add(player);
                                break;
                            case "leave":
                                var p = Model.PlayerList?.Single(player => player.Name == args.PlayerName);
                                Model.PlayerList?.Remove(p);
                                break;

                        }
                        PlayerAction?.Invoke(this, args);
                        break;
                }
            }
            
        }
    }
}
