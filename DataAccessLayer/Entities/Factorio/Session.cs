namespace DataAccessLayer.Entities.Factorio;

public partial class Session
{
    public int Id { get; set; }

    public DateTime StartTimestamp { get; set; }

    public DateTime? StopTimestamp { get; set; }

    public virtual ICollection<Player> Players { get; } = new List<Player>();
}
