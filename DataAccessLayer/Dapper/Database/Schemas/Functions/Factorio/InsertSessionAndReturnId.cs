using Dapper;
using DataAccessLayer.Dapper.Database.Interfaces;
using Npgsql;

namespace DataAccessLayer.Dapper.Database.Schemas.Functions.Factorio
{
    public class InsertSessionAndReturnId : IFunction

    {
        public T Run<T>()
        {
            using var connection = new NpgsqlConnection("");
            connection.Open();

            var query = string.Format("SELECT factorio.insert_session_and_return_id('{0}')", DateTime.Now);
            var rowCount = connection.ExecuteScalar<T>(query);
            return rowCount;
        }


    }
}
