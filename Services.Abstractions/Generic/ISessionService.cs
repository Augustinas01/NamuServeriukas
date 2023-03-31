
namespace Services.Abstractions.Generic
{
    public interface ISessionService
    {
        Task<int> StartSession();
        Task StopSession(int sessionId);
    }
}
