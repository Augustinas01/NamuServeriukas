using Contracts.Configuration.Infrastructure;
using Domain.Repositories.General;
using Microsoft.EntityFrameworkCore;
using PostgresDatabase.Contexts;
using PostgresDatabase.Mapping;

namespace PostgresDatabase.Repositories.Config
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly PostgresDbContext _dbContext;

        public ServiceRepository(PostgresDbContext ctx) => _dbContext = ctx;

        public async Task<ServiceDto> GetById(int id)
        {
            var srv = await _dbContext.Services.SingleAsync(s => s.Id == id);
            return ServiceMapper.Map(srv);
        }

        public async Task<List<ServiceDto>> GetAllAsync()
        {
            var list = await  _dbContext.Services.ToListAsync();
            return list.Select(s => ServiceMapper.Map(s)).ToList();
        }

        public async Task<ServiceDto> GetByNameAsync(string name)
        {
            var e = await _dbContext.Services.SingleAsync(s => s.Name == name);
            return ServiceMapper.Map(e);
        }

        public async Task<List<ServiceDto>> GetByTypeAsync(string type)
        {
            var list = await _dbContext.Services.Where(e =>  e.Type == type).ToListAsync();
            return list.Select(s => ServiceMapper.Map(s)).ToList();
        }
    }
}
