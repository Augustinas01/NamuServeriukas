using Contracts.Generic.Session.Infrastructure;
using Contracts.Generic.User;
using Enums;

namespace Contracts.Configuration.Presentation
{
    public class ServicePresentationDto
    {
        public int Id { get; set; }
        public ServiceEnum.Type Type { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public List<ServiceUserDto>? ServiceUserList { get; set; }
        public DateTime StartedDateTimeUtc { get; set; }
        public DateTime StopedDateTimeUtc { get; set; }
    }
}
