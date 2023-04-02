using Enums.Models;
using ServerioAPI.Interfaces;

namespace ServerioAPI.Services.Information
{
    public class GamesInfoService : IGamesService
    {
        private readonly IConfiguration _configuration;
        private readonly IProcessService _processService;
        public GamesInfoService(IConfiguration configuration, IProcessService processService)
        {
            _configuration = configuration;
            _processService = processService;
        }
        private string GamesDirectory => _configuration["GamesDirectoryPath"];

        public List<GameModel> GetGamesList()
        {
            var list = Directory.GetDirectories(GamesDirectory)
                .Select(s => new GameModel() { Name = s.Substring(GamesDirectory.Length + 1) })
                .ToList();

            return list;
        }

        public List<GameModel> GetGamesListWithState()
        {
            var list = Directory.GetDirectories(GamesDirectory)
                .Select(s => new GameModel()
                { 
                    Name = s.Substring(GamesDirectory.Length + 1),
                    State = GetGameState(s.Substring(GamesDirectory.Length + 1))
                })
                .ToList();

            return list;
        }

        private string GetGameState(string gameName)
        {
            switch (gameName.ToLower())
            {
                case "factorio":
                    var val = _processService.GetServerBaseInfo().State;
                    return val != null ? val.ToString() : "Error getting factorio state";

                default: return "unknown";
            }
        }
    }
}
