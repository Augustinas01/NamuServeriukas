using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalProcesses.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime JoinTime { get; set; }
        public DateTime DisconnectTime { get; set; }
    }
}
