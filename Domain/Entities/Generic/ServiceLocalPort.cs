using Domain.Entities.General;

namespace Domain.Entities.Generic
{
    public class ServiceLocalPort
    {
        public int Id;
        public int Port;
        public int ServiceId;
        public Service? Service;

    }
}
