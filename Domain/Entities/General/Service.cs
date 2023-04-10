using Domain.Entities.Generic;

namespace Domain.Entities.General
{
    public class Service
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? PathToExe { get; set; }
        public string? ExeArgs { get; set; }

        public virtual ICollection<ServiceSession>? Sessions { get; set; }
   }
}
