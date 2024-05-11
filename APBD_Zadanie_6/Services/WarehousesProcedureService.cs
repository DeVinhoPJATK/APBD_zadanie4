using System.Data.SqlClient;
using APBD_Task_6.Models;

namespace Zadanie5.Services;

public class WarehousesProcedureService : IWarehousesProcedureService
{

    private readonly IConfiguration _configuration;

    public WarehousesProcedureService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<int> AddProductToWarehouseAsync(ProductWarehouse productWarehouse)
    {
        var connectionString = _configuration.GetValue<string>("ConnectionString");
        using var connection = new SqlConnection(connectionString);
        using var cmd = new SqlCommand("AddProductToWarehouse", connection);
        var transaction = (SqlTransaction)await connection.BeginTransactionAsync();

        try
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("IdProduct", productWarehouse.IdProduct);
            cmd.Parameters.AddWithValue("IdWarehouse", productWarehouse.IdWarehouse);
            cmd.Parameters.AddWithValue("Amount", productWarehouse.Amount);
            cmd.Parameters.AddWithValue("CreatedAt", productWarehouse.CreatedAt);

            await connection.OpenAsync();
            int rowsChanged = await cmd.ExecuteNonQueryAsync();

            if (rowsChanged < 1) throw new Exception();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw new Exception();
        }
        cmd.Parameters.Clear();

        cmd.CommandText = "SELECT TOP 1 IdProductWarehouse FROM Product_Warehouse ORDER BY IdProductWarehouse DESC";

        using var reader = await cmd.ExecuteReaderAsync();
        int idProductWarehouse = int.Parse(reader["IdProductWarehouse"].ToString());
        await reader.CloseAsync();
        return idProductWarehouse;
    }
}
