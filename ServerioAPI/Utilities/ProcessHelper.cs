using BusinessLayer.Enumerators;
using System.Text.RegularExpressions;
using System;
using BusinessLayer.Enumerators.Factorio;

namespace ServerioAPI.Utilities
{
    public class ProcessHelper
    {
        public static ProcessEnum.Type DeterminateProcessType(object o)
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

        public static FactorioOutputEnum.LineType DeterminateProcessOutputLineType(string plainLine)
        {
            plainLine = plainLine.Trim();
            if (Regex.IsMatch(plainLine, @"^\d{4}-\d{2}-\d{2}"))
            {
                return FactorioOutputEnum.LineType.FactorioDate;
            }
            else if(Regex.IsMatch(plainLine,@"^\d*\.\d{3}"))
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
    }
}
