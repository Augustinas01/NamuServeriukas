namespace ExternalProcesses.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public string? State { get; set; }
        public DateTime StartTime { get; set; }
        public List<PlayerModel>? PlayerList { get; set; }
    }
}
