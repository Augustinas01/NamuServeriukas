using Enums;
using Enums.Factorio;
using Enums.Models.Output.Factorio;
using ExternalProcesses.Handlers.EventArgsModels;
using ExternalProcesses.Models;
using Services.Abstractions.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ExternalProcesses.Handlers
{
    internal class ProcessOutputHandler : IProcessOutputHandler
    {
        public event EventHandler<ServerStateChangedEventArgs> ServerStateChanged = delegate { };
        public event EventHandler<PlayerActionEventArgs> PlayerAction = delegate { };
        public void ConsumeProcessOutput(object s, DataReceivedEventArgs e)
        {
            if(e != null)
            {
                ConsumeProcessOutput(e);
            }
        }

        public void ConsumeProcessOutput(DataReceivedEventArgs args)
        {
            if (args.Data != null)
            {
                var lineType = DeterminateProcessOutputLineType(args.Data);

                switch (lineType)
                {
                    case FactorioOutputEnum.LineType.FactorioTickInfo:
                        var lineWithoutTick = args.Data.Trim().Substring(args.Data.Trim().IndexOf(" "));
                        if (lineWithoutTick.Contains("changing state"))
                        {
                            var fromAndToString = lineWithoutTick.Substring(lineWithoutTick.IndexOf("state") + "state ".Length).Split(" ");
                            var fromState = fromAndToString[0].Substring(5).TrimEnd(')');
                            var toState = fromAndToString[1].Substring(3).TrimEnd(')');

                            switch (toState.ToLower())
                            {
                                case "creatinggame":
                                    OnServerStateChanged(new ServerStateChangedEventArgs() { State = "loading game"});
                                    //_serverState = "loading game";
                                    //_game.State = "loading game";
                                    break;
                                case "ingame":
                                    OnServerStateChanged(new ServerStateChangedEventArgs()
                                                                                { State = "online",
                                                                                  Time = DateTime.UtcNow
                                                                                });
                                    //_serverState = "online";
                                    //_serverOnline = true;
                                    //_game.State = "online";
                                    //_game.StartTime = DateTime.Now;
                                    break;
                                default:
                                    OnServerStateChanged(new ServerStateChangedEventArgs() { State = "warming-up" });
                                    //_serverState = "warming-up";
                                    //_game.State = "warming-up";
                                    return;
                            }
                        }
                        break;
                    case FactorioOutputEnum.LineType.FactorioDate:
                        var dateLine = ParseDateLine(args.Data);
                        switch (dateLine.Action)
                        {
                            case FactorioOutputEnum.LineAction.Join:
                                var a = new PlayerActionEventArgs()
                                {
                                    Action = "join",
                                    PlayerName = dateLine.PlayerName,
                                    Time = DateTime.UtcNow
                                };
                                OnPlayerAction(a);
                                //_game.PlayerList.Add(new PlayerModel()
                                //{
                                //    Name = dateLine.PlayerName,
                                //    JoinTime = DateTime.Now
                                //});
                                break;
                            case FactorioOutputEnum.LineAction.Leave:
                                var ar = new PlayerActionEventArgs()
                                {
                                    Action = "leave",
                                    PlayerName = dateLine.PlayerName,
                                    Time = DateTime.UtcNow
                                };
                                OnPlayerAction(ar);
                                //if (_game.PlayerList.Count > 0)
                                //{
                                //    var playerWhoLeft = _game.PlayerList.First(p => p.Name.Equals(dateLine.PlayerName, StringComparison.OrdinalIgnoreCase));
                                //    _game.PlayerList.Remove(playerWhoLeft);
                                //}
                                break;
                        }
                        break;
                    case FactorioOutputEnum.LineType.FactorioTickError:
                        OnServerStateChanged(new ServerStateChangedEventArgs()
                        {
                            State = "error",
                            Time = DateTime.UtcNow
                        });
                        //TOODO handling?
                        //_serverState = "Error";
                        //_game.State = "Error";
                        break;
                    default: return;
                }
            }

        }

        public ProcessEnum.Type DeterminateProcessType(object o)
        {
            var prcType = ProcessEnum.Type.Undefined;

            if (o != null)
            {
                var objectString = o.ToString();

                if (objectString != null)
                {
                    if (objectString.Contains("factorio"))
                    {
                        prcType = ProcessEnum.Type.Factorio;
                    }
                }
                else
                {
                    prcType = ProcessEnum.Type.Undefined;
                }

            }
            else
            {
                prcType = ProcessEnum.Type.None;
            }

            return prcType;
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

        private FactorioOutputEnum.LineType DeterminateProcessOutputLineType(string plainLine)
        {
            plainLine = plainLine.Trim();
            if (Regex.IsMatch(plainLine, @"^\d{4}-\d{2}-\d{2}"))
            {
                return FactorioOutputEnum.LineType.FactorioDate;
            }
            else if (Regex.IsMatch(plainLine, @"^\d*\.\d{3}"))
            {
                if (Regex.IsMatch(plainLine, @"^\d*\.\d{3} Info"))
                {
                    return FactorioOutputEnum.LineType.FactorioTickInfo;
                }
                if (Regex.IsMatch(plainLine, @"^\d*\.\d{3} Error"))
                {
                    return FactorioOutputEnum.LineType.FactorioTickError;
                }
                return FactorioOutputEnum.LineType.FactorioTick;
            }

            return FactorioOutputEnum.LineType.Undefined;
        }

        #region Events

        protected virtual void OnServerStateChanged(ServerStateChangedEventArgs e)
        {
            ServerStateChanged?.Invoke(this, e);
        }

        protected virtual void OnPlayerAction(PlayerActionEventArgs e)
        {
            PlayerAction?.Invoke(this, e);
        }
        #endregion
    }
}
