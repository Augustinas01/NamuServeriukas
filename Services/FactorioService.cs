
using Contracts.Factorio;
using Domain.Entities.Factorio;
using Domain.Repositories;
using Enums;
using Services.Abstractions.Facades;

namespace Services
{
    public class FactorioService : IFactorioService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IProcessService _processService;

        public FactorioService(
            IRepositoryManager repositoryManager,
            IProcessService processService)
        {
            _repositoryManager = repositoryManager;
            _processService = processService;
        }
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
            var sessionType = ProcessEnum.Type.Factorio;
            var session = new FactorioSession() { StartTimestamp = DateTime.UtcNow };

            await _processService.StartProcess(sessionType);

            _repositoryManager.FactorioRepository.InsertSession(session);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            _processService.SetServerId(sessionType, session.Id);

            return session.Id;

        }

        public async Task StopSession()
        {
            var sessionId = await _processService.StopProcess(ProcessEnum.Type.Factorio);

            var session = await _repositoryManager.FactorioRepository.GetSessionByIdAsync(sessionId) ?? throw new Exception("Nerasta sesija");
            session.StopTimestamp = DateTime.UtcNow;
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

        }


    }
}
