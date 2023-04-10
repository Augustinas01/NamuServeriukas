
using Enums.Enumerators;
using DataAccessLayer.Dapper.Database.Schemas.Tables.Factorio;

namespace DataAccessLayer.Dapper.Database.Schemas
{
    internal class FactorioSchema
    {
        private readonly SessionTable _sessionTable;

        public FactorioSchema()
        {
            _sessionTable = new SessionTable();
        }
        public int InsertSessionTimestamp(SessionEnum.Action a, int? sessionId)
        {
            return a switch
            {
                SessionEnum.Action.Start => _sessionTable.Insert(),
                SessionEnum.Action.Stop => _sessionTable.Update(sessionId) == 1 ? int.MaxValue : int.MinValue,
                _ => int.MinValue,
            };
        }
    }
}
