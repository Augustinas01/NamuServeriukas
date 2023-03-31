
using Contracts.Factorio;
using Domain.Entities.General;
using Domain.Repositories;
using Services.Abstractions.Factorio;

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
            var session = new GameSession() { StartTimestamp = DateTime.Now };

            _repositoryManager.GameSessionRepository.Insert(session);

            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            return session.Id;

        }

        public async Task StopSession(int sessionId)
        {
            var session = await _repositoryManager.GameSessionRepository.GetSessionByIdAsync(sessionId) ?? throw new Exception("Sesija nerasta");

            session.StopTimestamp = DateTime.Now;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();

        }


    }
}
