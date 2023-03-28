using Dapper;
using DataAccessLayer.Dapper.Database.Interfaces;
using DataAccessLayer.Dapper.Database.Schemas.Functions.Factorio;
using Npgsql;

namespace DataAccessLayer.Dapper.Database.Schemas.Tables.Factorio
{
    public class SessionTable : ITable
    {

        public int Insert()
        {
            var f = new InsertSessionAndReturnId();
            return f.Run<int>();
        }

        public int Update(int? id)
        {
            using var connection = new NpgsqlConnection("");
            connection.Open();

            var query = string.Format("UPDATE factorio.session SET stop_timestamp = '{0}' WHERE id = {1}", DateTime.Now, id);
            var parameters = new { timestamp = DateTime.Now };
            var rowCount = connection.Execute(query, parameters);
            return rowCount;
        }
    }
}
