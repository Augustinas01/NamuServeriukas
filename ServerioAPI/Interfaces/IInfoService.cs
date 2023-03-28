using BusinessLayer.Models;
using System.Diagnostics;

namespace ServerioAPI.Interfaces
{
    public interface IInfoService
    {
        public void SetServerStatus(bool online);
        public bool IsServerOnline();
        public string GetServerStatus();
        public List<PlayerModel>? GetPlayers();
        public GameModel GetGame();

        public void ConsumeProcessOutput(DataReceivedEventArgs args);
    }
}
