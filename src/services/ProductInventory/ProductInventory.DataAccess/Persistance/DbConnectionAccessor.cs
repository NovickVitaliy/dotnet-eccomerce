using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace ProductInventory.DataAccess.Persistance;

public class DbConnectionAccessor
{
    public const string ConnectionStringPosition = "SqlServer";
    private readonly string _connectionString;
    
    public DbConnectionAccessor(string connectionString)
    {
        _connectionString = connectionString;
    }

    public DbConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }
}