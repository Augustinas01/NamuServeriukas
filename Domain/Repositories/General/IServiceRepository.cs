using Contracts.Configuration.Infrastructure;

namespace Domain.Repositories.General
{
    public interface IServiceRepository
    {
        Task<ServiceDto> GetById(int id);
        Task<List<ServiceDto>> GetByTypeAsync(string type);
        Task<ServiceDto> GetByNameAsync(string name);
        Task<List<ServiceDto>> GetAllAsync();
    }
}
