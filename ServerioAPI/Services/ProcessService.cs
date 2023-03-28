using BusinessLayer.Enumerators;
using BusinessLayer.Models;
using DataAccessLayer.Context;
using DataAccessLayer.Dapper.Database;
using Microsoft.EntityFrameworkCore;
using ServerioAPI.Interfaces;
using ServerioAPI.Services.Information;
using ServerioAPI.Utilities;
using System.Diagnostics;


namespace ServerioAPI.Services
{
    public class ProcessService : IProcessService
    {
        private Process? _process;
        private readonly IConfiguration _configuration;
        private readonly IInfoService _infoService;
        private readonly PostgresContext _dbCtx;
        private ProcessEnum.Type _processType = ProcessEnum.Type.None;
        string? exePath;
        string? arguments;
        private int? SessionId;

        public ProcessService(IConfiguration configuration, PostgresContext dbContext)
        {
            _configuration = configuration;
            _infoService = new FactorioInfoService();
            _dbCtx = dbContext;
            SetUp();
        }

        private void SetUp()
        {
            exePath = _configuration["ExePath"];
            arguments = _configuration["Args"];

        }

        public void Start()
        {
            if (_process != null)
            {
                throw new InvalidOperationException("A process is already running.");
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = $"{exePath}",
                Arguments = $"{arguments}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = false
            };

            _process = new Process();
            _process.StartInfo = startInfo;
            _process.OutputDataReceived += ProcessOutputHandler;
            _process.Start();
            _process.BeginOutputReadLine();
            UpdateDatabase(SessionEnum.Action.Start);
        }

        public void Stop()
        {
            if (_process == null)
            {
                throw new InvalidOperationException("No process is currently running.");
            }

            _process.Kill();
            _process.Dispose();
            _process = null;
            _processType = ProcessEnum.Type.None;
            _infoService.SetServerStatus(false);
            UpdateDatabase(SessionEnum.Action.Stop);
        }

        public ServerInfo GetServerBaseInfo()
        {
            return new ServerInfo
            {
                Name = _processType.ToString(),
                IsOnline = _infoService.IsServerOnline(),
                State = _infoService.GetServerStatus()
            };
        }

        public GameModel GetGameInfo()
        {
            return _infoService.GetGame();
        }

        private void ProcessOutputHandler(object s, DataReceivedEventArgs e)
        {

            if (s != null && _processType == ProcessEnum.Type.None)
            {
                _processType = ProcessHelper.DeterminateProcessType(s);
            }

            if (e != null)
            {
                switch (_processType)
                {
                    case ProcessEnum.Type.Factorio:
                        _infoService.ConsumeProcessOutput(e);
                        break;
                    default: return;
                }
            }
        }

        private void UpdateDatabase(SessionEnum.Action sessionType)
        {
            switch (sessionType)
            {
                case SessionEnum.Action.Start:
                    var s = new DataAccessLayer.Entities.Factorio.Session { StartTimestamp = DateTime.Now };
                    _dbCtx.Sessions.Add(s);
                    _dbCtx.SaveChanges();
                    SessionId = s.Id;
                    break;
                case SessionEnum.Action.Stop:
                    var session = _dbCtx.Sessions.Find(SessionId);
                    if (session != null)
                    {
                        session.StopTimestamp = DateTime.Now;
                    }
                    _dbCtx.SaveChanges();

                    SessionId = null;
                    break;
            }
            
        }

    }
}
