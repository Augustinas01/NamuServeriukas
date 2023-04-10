
namespace Contracts.Configuration.Infrastructure
{
    public class ServiceLaunchDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? PathToExe { get; set; }
        public string? ExeArgs { get; set; }
    }
}
