namespace ExternalProcesses.Models
{
    public class GameServerModel
    {
        public int Id { get; set; }
        public string? State { get; set; }
        public DateTime StartTime { get; set; }
        public List<PlayerModel>? PlayerList { get; set; }
    }
}
