
using Enums;

namespace Services.Abstractions.Generic
{
    public interface ISessionService
    {
        Task<int> Start(int id);
        Task Stop(int id);
    }
}
