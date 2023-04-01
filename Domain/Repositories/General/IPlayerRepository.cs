namespace Domain.Repositories.General
{
    public interface IPlayerRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllPlayersAsync();
        Task<T> GetPlayerByIdAsync(int id);
        void InsertPlayer(T entity);
        void UpdatePlayer(T entity);
    }
}
