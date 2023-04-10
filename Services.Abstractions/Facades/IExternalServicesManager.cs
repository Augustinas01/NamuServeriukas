
using Contracts.Generic.Service;
using Services.Abstractions.Generic;

namespace Services.Abstractions.Facades
{
    public interface IExternalServicesManager : ISessionService
    {
        ServiceModel GetServiceInfo(int id);
    }
}
