using Microsoft.Extensions.Hosting;
using Services.Abstractions.Facades;

namespace Services.Abstractions
{
    public interface IServiceManager 
    {
        IExternalServicesManager ExternalServicesManager { get; }
        IExternalServicesLibrary ExternalServicesLibrary { get; }
    }
}
