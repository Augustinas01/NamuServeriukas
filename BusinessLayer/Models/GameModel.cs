namespace Enums.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? State { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public List<PlayerModel>? PlayerList { get; set; }
    }
}
