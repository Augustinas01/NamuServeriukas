
namespace Services.Abstractions.Generic
{
    public interface IPlayerService<T> where T : class
    {
        Task<T> CreatePlayerAsync();
        Task<T> UpdatePlayerAsync(T player);
    }
}
