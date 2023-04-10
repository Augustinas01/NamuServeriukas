using Enums;

namespace Contracts
{
    public class ExternalServiceArgs : EventArgs
    {
        public ServiceEnum.Action ServiceAction { get; set; }
        public string? State { get; set; }
        public DateTime Time { get; set; }
        public string? Action { get; set; }
        public string? PlayerName { get; set; }
    }
}
