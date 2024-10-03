using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using ProductInventory.DataAccess.Persistance;
using ProductInventory.DataAccess.Repositories.Contracts;
using ProductInventory.Domain.Exceptions;
using ProductInventory.Domain.Models;

namespace ProductInventory.DataAccess.Repositories.Implementations;

public class SupplierRepository : ISupplierRepository
{
    private readonly DbConnectionAccessor _dbConnectionAccessor;

    public SupplierRepository(DbConnectionAccessor dbConnectionAccessor)
    {
        _dbConnectionAccessor = dbConnectionAccessor;
    }

    public async Task<int> CreateSupplierAsync(Supplier supplier)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        await connection.OpenAsync();
        await using var cmd = connection.CreateCommand();
        BuildInsertCommand(supplier, cmd);

        var id = (long?)await cmd.ExecuteScalarAsync();
        return (int)id!;
    }

    private static void BuildInsertCommand(Supplier supplier, DbCommand cmd)
    {
        cmd.CommandText = "EXECUTE sp_InsertSupplier @name = @name, @contactInfo = @contactInfo, @address = @address; SELECT CAST(SCOPE_IDENTITY() AS INT);";
        cmd.Parameters.Add(new SqlParameter()
        {
            ParameterName = "@name",
            SqlDbType = SqlDbType.VarChar,
            Value = supplier.Name,
            Direction = ParameterDirection.Input
        });

        cmd.Parameters.Add(new SqlParameter()
        {
            ParameterName = "@contactInfo",
            SqlDbType = SqlDbType.VarChar,
            Value = supplier.ContactInfo,
            Direction = ParameterDirection.Input
        });

        cmd.Parameters.Add(new SqlParameter()
        {
            ParameterName = "@address",
            SqlDbType = SqlDbType.VarChar,
            Value = supplier.Address,
            Direction = ParameterDirection.Input
        });
    }

    public async Task<Supplier?> GetSupplierByIdAsync(int supplierId)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        await connection.OpenAsync();
        await using var cmd = new SqlCommand("SELECT * FROM Supplier WHERE SupplierId = @supplierId", (SqlConnection)connection);
        cmd.Parameters.Add(new SqlParameter()
        {
            ParameterName = "@supplierId",
            Value = supplierId,
        });
        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            var supplier = await ReadSupplier(reader);
            return supplier;
        }

        throw new SupplierNotFoundException();
    }
    private ValueTask<Supplier> ReadSupplier(SqlDataReader reader)
    {
        var address = reader.GetString("Address");
        var name = reader.GetString("Name");
        var contactInfo = reader.GetString("ContactInfo");
        var supplierId = reader.GetInt32("SupplierId");

        return ValueTask.FromResult(new Supplier()
        {
            Address = address,
            Name = name,
            ContactInfo = contactInfo,
            SupplierId = supplierId
        });
    }

    public async Task<List<Supplier>> GetSuppliersAsync(int pageNumber, int pageSize)
    {
        List<Supplier> suppliers = [];
        await using var connection = _dbConnectionAccessor.GetConnection();
        await connection.OpenAsync();
        await using var cmd = new SqlCommand(
            "SELECT * FROM Supplier ORDER BY SupplierId" +
            " OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY", (SqlConnection)connection);

        var skip = (pageNumber - 1) * pageSize;
        var take = pageSize;

        cmd.Parameters.Add(new SqlParameter()
        {
            ParameterName = "@skip",
            Value = skip
        });

        cmd.Parameters.Add(new SqlParameter()
        {
            ParameterName = "@take",
            Value = take
        });

        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            suppliers.Add(await ReadSupplier(reader));
        }

        return suppliers;
    }

    public async Task<bool> DeleteSupplierAsync(int supplierId)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        await connection.OpenAsync();
        await using var cmd = new SqlCommand("DELETE FROM Supplier WHERE SupplierId = @supplierId", (SqlConnection)connection);
        cmd.Parameters.Add(new SqlParameter()
        {
            ParameterName = "@supplierId",
            Value = supplierId
        });

        var rowsAffected = await cmd.ExecuteNonQueryAsync();

        return rowsAffected == 1;
    }

    public async Task<bool> UpdateSupplierAsync(Supplier supplier)
    {
        await using var connection = _dbConnectionAccessor.GetConnection();
        await connection.OpenAsync();
        await using var cmd = new SqlCommand(
            "UPDATE Supplier SET " +
            "Name = @name, Address = @address, ContactInfo = @contactInfo WHERE SupplierId = @supplierId", (SqlConnection)connection);

        cmd.Parameters.Add(new SqlParameter()
        {
            ParameterName = "@name",
            Value = supplier.Name
        });
        cmd.Parameters.Add(new SqlParameter()
        {
            ParameterName = "@address",
            Value = supplier.Address
        });
        cmd.Parameters.Add(new SqlParameter()
        {
            ParameterName = "@contactInfo",
            Value = supplier.ContactInfo
        });
        cmd.Parameters.Add(new SqlParameter()
        {
            ParameterName = "@supplierId",
            Value = supplier.SupplierId
        });

        var rowsAffected = await cmd.ExecuteNonQueryAsync();
        return rowsAffected == 1;
    }
}