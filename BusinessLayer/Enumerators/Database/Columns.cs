
namespace Enums.Enumerators.Database
{
    public class Columns
    {
        public enum Session
        {
            id,
            start_timestamp,
            stop_timestamp
        }

        public enum Player
        {
            id,
            join_timestamp,
            leave_timestamp,
            session_id
        }
    }
}
