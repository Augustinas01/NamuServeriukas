namespace BusinessLayer.Enumerators.Factorio
{
    public class FactorioOutputEnum
    {
        public enum LineType
        {
            Undefined,
            FactorioTick,
            FactorioDate,
            FactorioTickInfo,
            FactorioTickError
        }

        public enum LineAction
        {
            Undefined,
            Join,
            Leave
        }
    }
}
