﻿namespace Domain.Repositories.General
{
    public interface IPlayerRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        void Insert(T entity);
        void Update(T entity);
    }
}
