

using BusinessLayer.Enumerators;
using DataAccessLayer.Dapper.Database.Schemas;

namespace DataAccessLayer.Dapper.Database
{
    public class PostgresDatabase
    {
        private readonly FactorioSchema _factorioSchema;
        public PostgresDatabase()
        {
            _factorioSchema = new FactorioSchema();
        }
        public int InsertSessionTimestamp(ProcessEnum.Type processType, SessionEnum.Action sessionAction, int? sessionId)
        {
            switch (processType)
            {
                case ProcessEnum.Type.Factorio:
                    var val = _factorioSchema.InsertSessionTimestamp(sessionAction, sessionId);
                    return val != int.MinValue ? val : throw new Exception();
                default: throw new Exception(string.Format("Unknown process to Database: {0}", processType));
            }
        }
    }
}
