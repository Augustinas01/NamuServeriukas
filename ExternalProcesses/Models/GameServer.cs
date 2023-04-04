
using Enums;
using ExternalProcesses.Handlers;
using ExternalProcesses.Handlers.EventArgsModels;
using System.Diagnostics;

namespace ExternalProcesses.Models
{
    public class GameServer : Process
    {
        public ProcessEnum.Type Type { get; set; }
        private GameModel Model { get; set; } = new();
        private ProcessOutputHandler ProcessOutputHandler { get; set; }

        public GameServer() 
        {
            ProcessOutputHandler = new();
            base.OutputDataReceived += ProcessOutputHandler.ConsumeProcessOutput;
            ProcessOutputHandler.ServerStateChanged += o_ServerDidAction;
            ProcessOutputHandler.PlayerAction += o_PlayerDidAction;
        }

        public void SetServerId(int id)
        {
            Model.Id = id;
        }
        public int GetServerId() => Model.Id;

        private void o_ServerDidAction(object sender, ServerStateChangedEventArgs e)
        {

        }

        private void o_PlayerDidAction(object sender, EventArgs e)
        {

        }

    }
}
