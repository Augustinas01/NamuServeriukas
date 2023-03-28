using BusinessLayer.Models;

namespace ServerioAPI.Interfaces
{
    public interface IGamesService
    {
        public List<GameModel> GetGamesList();
        public List<GameModel> GetGamesListWithState();
    }
}
