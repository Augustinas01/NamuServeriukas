﻿
using Enums.Factorio;

namespace Enums.Models.Output.Factorio
{
    public class FactorioDateLine
    {
        public DateTime Date { get; set; }
        public FactorioOutputEnum.LineAction Action { get; set; }
        public string? PlayerName { get; set; }
    }
}
