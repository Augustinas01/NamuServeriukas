namespace Enums.Factorio
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

        public enum Sender
        {
            None,
            Undefined,
            AppManager,
            AppManagerStates,
            CommandLineMultiplayer,
            GameActionHandler,
            PlayerData,
            Scenario,
            ServerMultiplayerManager,
            ServerRouter,
            ServerSynchronizer,
            UDPSocket

        }
    }
}
