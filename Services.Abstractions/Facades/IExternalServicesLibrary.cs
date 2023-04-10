using Contracts.Configuration.Infrastructure;
using Services.Abstractions.Generic;

namespace Services.Abstractions.Facades
{
    public interface IExternalServicesLibrary : ISystemExplorer
    {
        List<ServiceDto> GetAvailableServices();
        Task<List<ServiceDto>> GetConfiguredServices();
    }
}
