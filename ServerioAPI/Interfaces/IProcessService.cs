using Enums.Models;

namespace ServerioAPI.Interfaces
{
    public interface IProcessService
    {
        void Start();
        void Stop();
        public ServerInfo GetServerBaseInfo();
        public GameModel GetGameInfo();
    }
}
