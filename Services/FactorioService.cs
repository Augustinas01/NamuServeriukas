
using Contracts.Factorio;
using Domain.Entities.Factorio;
using Domain.Repositories;
using Services.Abstractions.Facades;

namespace Services
{
    public class FactorioService : IFactorioService
    {
        private readonly IRepositoryManager _repositoryManager;

        public FactorioService(IRepositoryManager repositoryManager) => _repositoryManager = repositoryManager;
        public Task<FactorioPlayerDto> CreatePlayerAsync()
        {
            throw new NotImplementedException();
        }
        public Task<FactorioPlayerDto> UpdatePlayerAsync(FactorioPlayerDto player)
        {
            throw new NotImplementedException();
        }
        public async Task<int> StartSession()
        
        {
            var session = new FactorioSession() { StartTimestamp = DateTime.UtcNow };

            _repositoryManager.FactorioRepository.InsertSession(session);

            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            return session.Id;

        }

        public async Task StopSession(int sessionId)
        {

            var session = await _repositoryManager.FactorioRepository.GetSessionByIdAsync(sessionId) ?? throw new Exception("Nerasta sesija");

            session.StopTimestamp = DateTime.UtcNow;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();

        }


    }
}
