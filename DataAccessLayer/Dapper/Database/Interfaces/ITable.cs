namespace DataAccessLayer.Dapper.Database.Interfaces
{
    internal interface ITable
    {
        public int Insert();
        public int Update(int? id);
    }
}
