
using System.Diagnostics;
using System.Text.RegularExpressions;
using BusinessLayer.Enumerators.Factorio;
using BusinessLayer.Models;
using BusinessLayer.Models.Output.Factorio;
using ServerioAPI.Interfaces;
using ServerioAPI.Utilities;

namespace ServerioAPI.Services.Information
{
    public class FactorioInfoService : IInfoService
    {
        private bool _serverOnline;
        private string _serverState = "offline";
        private GameModel _game = new() { Name = "Factorio", PlayerList = new() };

        public void SetServerStatus(bool online) 
        {
            _serverState = online ? _serverState : "offline";
            _serverOnline = online;
            _game.State = _serverState;
        }
        public bool IsServerOnline() { return _serverOnline; }
        public string GetServerStatus() { return _serverState; }
        public List<PlayerModel>? GetPlayers() {  return _game.PlayerList; }
        public GameModel GetGame() { return _game; }

        public void ConsumeProcessOutput(DataReceivedEventArgs args) 
        {
            if (args.Data != null)
            {
                var lineType = ProcessHelper.DeterminateProcessOutputLineType(args.Data);

                switch (lineType)
                {
                    case FactorioOutputEnum.LineType.FactorioTickInfo:
                        var lineWithoutTick = args.Data.Trim().Substring(args.Data.Trim().IndexOf(" "));
                        if (lineWithoutTick.Contains("changing state"))
                        {
                            var fromAndToString = lineWithoutTick.Substring(lineWithoutTick.IndexOf("state") + "state ".Length).Split(" ");
                            var fromState = fromAndToString[0].Substring(5).TrimEnd(')');
                            var toState = fromAndToString[1].Substring(3).TrimEnd(')');

                            switch(toState.ToLower())
                            {
                                case "creatinggame":
                                    _serverState = "loading game";
                                    _game.State = "loading game";
                                    break;
                                case "ingame":
                                    _serverState = "online";
                                    _serverOnline = true;
                                    _game.State = "online";
                                    _game.StartTime = DateTime.Now;
                                    break;
                                default: 
                                    _serverState = "warming-up";
                                    _game.State = "warming-up";
                                    return;
                            }
                        }
                        break;
                    case FactorioOutputEnum.LineType.FactorioDate:
                        var dateLine = ParseDateLine(args.Data);
                        switch (dateLine.Action)
                        {
                            case FactorioOutputEnum.LineAction.Join:
                                _game.PlayerList.Add(new PlayerModel()
                                {
                                    Name = dateLine.PlayerName,
                                    JoinTime = DateTime.Now
                                });
                                break;
                            case FactorioOutputEnum.LineAction.Leave:
                                if (_game.PlayerList.Count > 0)
                                {
                                    var playerWhoLeft = _game.PlayerList.First(p => p.Name.Equals(dateLine.PlayerName, StringComparison.OrdinalIgnoreCase));
                                    _game.PlayerList.Remove(playerWhoLeft);
                                }
                                break;
                        }
                        break;
                    case FactorioOutputEnum.LineType.FactorioTickError:
                        //TOODO handling?
                        _serverState = "Error";
                        _game.State = "Error";
                        break;
                    default: return;
                }
            }

        }

        private FactorioDateLine ParseDateLine(string line)
        {
            var arr = line.Trim().Split(' ');
            if (arr.Length > 4)
            {
                var actionType = arr[2].ToLower() switch
                {
                    "[join]" => FactorioOutputEnum.LineAction.Join,
                    "[leave]" => FactorioOutputEnum.LineAction.Leave,
                    _ => FactorioOutputEnum.LineAction.Undefined,
                };
                return new FactorioDateLine()
                {
                    Date = DateTime.Parse(arr[0] + ' ' + arr[1]),
                    Action = actionType,
                    PlayerName = arr[3]
                };
            }
            else
            {
                return new FactorioDateLine();
            }

        }
    }
}
