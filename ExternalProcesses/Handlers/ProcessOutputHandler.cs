using Contracts;
using Enums;
using Enums.Factorio;
using Enums.Models.Output.Factorio;
using Services.Abstractions.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ExternalProcesses.Handlers
{
    public class ProcessOutputHandler : IProcessOutputHandler
    {
        public event EventHandler<ExternalServiceArgs> ServerStateChanged = delegate { };
        public void ConsumeProcessOutput(object? s, DataReceivedEventArgs e)
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
                var outArgs = new ExternalServiceArgs()
                {
                    ServiceAction = lineType == FactorioOutputEnum.LineType.FactorioTickInfo ? ServiceEnum.Action.ServiceStateChanged : ServiceEnum.Action.PlayerAction
                };

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
                                    outArgs.State = "loading game";
                                    break;
                                case "ingame":

                                    outArgs.State = "online";
                                    outArgs.Time = DateTime.UtcNow;
                                    break;
                                default:
                                    outArgs.State = "warming-up";
                                    break;
                            }

                            OnServerAction(outArgs);
                        }
                        break;
                    case FactorioOutputEnum.LineType.FactorioDate:
                        var dateLine = ParseDateLine(args.Data);
                        switch (dateLine.Action)
                        {
                            case FactorioOutputEnum.LineAction.Join:

                                outArgs.Action = "join";
                                outArgs.PlayerName = dateLine.PlayerName;
                                outArgs.Time = DateTime.UtcNow;
                                break;
                            case FactorioOutputEnum.LineAction.Leave:

                                outArgs.Action = "leave";
                                outArgs.PlayerName = dateLine.PlayerName;
                                outArgs.Time = DateTime.UtcNow;
                                break;
                        }
                        OnServerAction(outArgs);

                        break;
                    case FactorioOutputEnum.LineType.FactorioTickError:

                        outArgs.State = "error";
                        outArgs.Time = DateTime.UtcNow;
                        OnServerAction(outArgs);

                        Console.WriteLine(String.Format("Factorio error: {0}",args.Data));
                        //TODO handling?
                        break;
                }

            }

        }

        public ServiceEnum.Name DeterminateProcessType(object o)
        {
            var prcType = ServiceEnum.Name.Undefined;

            if (o != null)
            {
                var objectString = o.ToString();

                if (objectString != null)
                {
                    if (objectString.Contains("factorio"))
                    {
                        prcType = ServiceEnum.Name.Factorio;
                    }
                }
                else
                {
                    prcType = ServiceEnum.Name.Undefined;
                }

            }
            else
            {
                prcType = ServiceEnum.Name.None;
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

        protected virtual void OnServerAction(ExternalServiceArgs e)
        {
            ServerStateChanged.Invoke(this, e);
        }

        #endregion
    }
}
